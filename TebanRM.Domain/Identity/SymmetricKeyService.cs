using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TebanRM.Application.Options;

namespace TebanRM.Application.Identity;
public class SymmetricKeyService
{
    private readonly string _key;

    public SymmetricKeyService(IOptions<SymmetricKeyOptions> options)
    {
        _key = options.Value.SymmetricKey;
    }

    public SymmetricSecurityKey GetSymmetricKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
    }
}
