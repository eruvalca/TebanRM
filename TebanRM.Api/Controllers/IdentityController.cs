using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TebanRM.Application.Identity;

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
        public Task<IActionResult> RegisterAsync(string firstName, string lastName, string email, string password)
        {
            var registerResult = _identityService.RegisterUserAsync(firstName, lastName, email, password);

        }
    }
}
