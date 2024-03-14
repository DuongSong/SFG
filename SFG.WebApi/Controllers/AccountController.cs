using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFG.Core.Domains.Account;
using SFG.Core.Services.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SFG.WebApi.Controllers
{
    [Route("sfg/account")]
    public class AccountController : Controller
    {
        readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            var result = await accountService.Login(request);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]AccountRegisterRequest request)
        {
            var result = await accountService.Register(request);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("get-role")]
        public async Task<IActionResult> GetRole()
        {
            var result = await accountService.GetAllUserRole();
            return Ok(result);
        }

        [HttpPost("role")]
        public async Task<IActionResult> CreateUserRole(UserRoleRequest request)
        {
            var result = await accountService.CreateUserRole(request);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var result = await accountService.ChangePassword(request);
            return Ok(result);
        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword()
        {
            var result = await accountService.ResetPassword("");
            return Ok(result);
        }

        [Authorize]
        [HttpPost("update-user-profile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileRequest request)
        {
            var result = await accountService.UpdateUserProfile(request);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("get-user-profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var result = await accountService.GetUserProfile();
            return Ok(result);
        }

        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var result = await accountService.RefreshToken();
            return Ok(result);
        }
    }
}

