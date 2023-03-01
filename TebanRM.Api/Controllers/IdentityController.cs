using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TebamRM.Contracts.Requests;
using TebanRM.Application.Identity;
using TebanRM.Application.Models;

namespace TebanRM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityService _identityService;

        public IdentityController(IdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var tebanUser = new TebanUser
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var registerResult = await _identityService.RegisterUserAsync(tebanUser, request.Password);
            if(registerResult.Item1 == true)
            {
                return Ok(registerResult.Item2);
            }
            else
            {
                return BadRequest(registerResult.Item2);
            }
        }
    }
}
