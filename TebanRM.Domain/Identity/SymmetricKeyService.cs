using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TebanRM.Domain.Identity;
public class SymmetricKeyService
{
    private readonly IConfiguration _config;

    public SymmetricKeyService(IConfiguration config)
    {
        _config = config;
    }

    public SymmetricSecurityKey GetSymmetricKey()
    {
        var symmetricKey = _config["Teban:SymmetricKey"];

        return symmetricKey == null
            ? throw new NullReferenceException("Symmetric Key configuration value is missing.")
            : new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricKey));
    }
}
