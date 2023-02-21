using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TebanRM.Domain.Entities;
using TebanRM.Domain.Identity.Dtos;

namespace TebanRM.Domain.Identity;
public class IdentityService
{
    private readonly IConfiguration _config;
    private readonly UserManager<TebanUser> _userManager;
    private readonly SymmetricKeyService _symmetricKeyService;

    public IdentityService(IConfiguration config,
        UserManager<TebanUser> userManager,
        SymmetricKeyService symmetricKeyService)
    {
        _config = config;
        _userManager = userManager;
        _symmetricKeyService = symmetricKeyService;
    }

    public async Task<(bool, string)> RegisterUserAsync(RegisterDto registerDto)
    {
        var user = new TebanUser
        {
            Email = registerDto.Email,
            UserName = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "General");
            return (true, user.Id);
        }

        if (result.Errors is not null)
        {
            return (false, result.Errors.First().Description);
        }

        return (false, "Something went wrong during registration.");
    }

    public async Task<(bool, string)> LoginUserAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user is null)
        {
            return (false, "User does not exist.");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!isPasswordValid)
        {
            return (false, "Invalid password.");
        }

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, loginDto.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName is null ? string.Empty : user.UserName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

        var userRoles = await _userManager.GetRolesAsync(user);

        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var symmetricKey = _symmetricKeyService.GetSymmetricKey();

        var issuer = _config["Teban:Issuer"];
        var audience = _config["Teban:Audience"];

        if (issuer is null || audience is null)
        {
            throw new NullReferenceException("Either the issuer configuration value or the audience configuration value or both are missing.");
        }

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddDays(14),
            signingCredentials: new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return (true, tokenString);
    }

    public async Task<string> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        if (user.UserName is null)
        {
            throw new NullReferenceException("This user does not have a username set.");
        }

        return user.UserName;
    }

    public async Task<(bool, string)> UpdateUserAsync(TebanUser user)
    {
        var thisUser = await _userManager.Users.FirstAsync(u => u.Id == user.Id);

        thisUser.FirstName = user.FirstName;
        thisUser.LastName = user.LastName;

        var result = await _userManager.UpdateAsync(thisUser);

        if (result.Succeeded)
        {
            return (true, thisUser.Id);
        }

        if (result.Errors is not null)
        {
            return (false, result.Errors.First().Description);
        }

        return (false, "Something went wrong updating user details.");
    }

    public async Task<(bool, IEnumerable<TebanUser>)> GetAllUsersAsync()
    {
        var allUsers = await _userManager.Users.ToListAsync();

        if (allUsers.Any())
        {
            return (true, allUsers);
        }

        return (false, new List<TebanUser>());
    }
}
