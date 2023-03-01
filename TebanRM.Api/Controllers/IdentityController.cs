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
        public async Task<IActionResult> RegisterAsync(string firstName, string lastName, string email, string password)
        {
            var registerResult = await _identityService.RegisterUserAsync(firstName, lastName, email, password);
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
