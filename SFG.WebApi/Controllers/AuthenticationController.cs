
using Microsoft.AspNetCore.Mvc;
using SFG.Core.Services.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SFG.WebApi.Controllers
{
    [Route("sfg/authentication")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [Route("check-refresh-token")]
        [HttpGet]
        public async Task<IActionResult> CheckRefreshToken()
        {
            var result = await authenticationService.CheckRefreshToken();

            return result ? Ok(result) : Unauthorized();
        }
    }
}

