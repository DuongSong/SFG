using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SFG.Core.Commons;
using SFG.Core.Constants;
using SFG.Core.Domains.Account;
using SFG.Core.Repositories.Interfaces;
using SFG.Core.Services.Interfaces;
using SFG.Core.Settings;
using SFG.Core.Entities.Accounts;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace SFG.Core.Services.Implementations
{
    public class AccountService : IAccountService
	{
        private readonly IAuthenticationService authenticationService;
        private readonly UserManager<UserProfile> userManager;
        private readonly ILogger<IAccountService> logger;
        private readonly JwtTokenSetting jwtTokenSetting;
        private readonly IRepository<IdentityRole, string> identityRoleRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICacheService cacheService;

        public AccountService(UserManager<UserProfile> userManager,
            IRepository<IdentityUser, string> identityUserRepository,
            IRepository<IdentityRole, string> identityRoleRepository,
            IOptions<JwtTokenSetting> jwtTokenSetting,
            ILogger<IAccountService> logger,
            IAuthenticationService authenticationService,
            IHttpContextAccessor httpContextAccessor,
            ICacheService cacheService)
		{
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.identityRoleRepository = identityRoleRepository;
            this.jwtTokenSetting = jwtTokenSetting.Value;
            this.logger = logger;
            this.authenticationService = authenticationService;
            this.cacheService = cacheService;
        }


        public async Task<bool> Login(LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Username);

            var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                throw new CustomExceptionHandler(AccountMessages.LOGIN_FAILED);
            }

            var userRoles = await userManager.GetRolesAsync(user);

            var accessToken = GenerateAccessToken(user, userRoles);
            httpContextAccessor?.HttpContext?.Response.Cookies.Append(
                "accessToken",
                accessToken,
                new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Lax, Expires = DateTime.Now.AddMinutes(jwtTokenSetting.ShortExpiryMinutes) });

            var refreshToken = GenerateRefreshToken();
            httpContextAccessor?.HttpContext?.Response.Cookies.Append(
                "refreshToken",
                refreshToken,
                new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Lax, Expires = DateTime.Now.AddMinutes(jwtTokenSetting.LongExpiryMinutes) });

            HashEntry[] hashes =
            {
                new HashEntry("refreshToken", refreshToken),
                new HashEntry("expired", DateTime.Now.AddMinutes(jwtTokenSetting.LongExpiryMinutes).ToString())
            };
            await cacheService.SetCache(user.Email, hashes);

            return true;
        }

        public async Task<IdentityResult> Register(AccountRegisterRequest request)
        {
            var user = new UserProfile(request);

            var result = await userManager.CreateAsync(user, request.Password);

            result = await userManager.AddToRoleAsync(user, request.UserType);

            return result;
        }

        public async Task<string> CreateUserRole(UserRoleRequest request)
        {
            var identityRole = new IdentityRole
            {
                ConcurrencyStamp = string.Empty,
                NormalizedName = request.RoleName,
                Name = request.RoleName
            };

            var result = await identityRoleRepository.AddEntity(identityRole);

            return result > 0 ? "Success." : throw new CustomExceptionHandler("Failed.");
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Username);

            var result = await userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);

            return result;
        }

        public async Task<IdentityResult> UpdateUserProfile(UserProfileRequest request)
        {
            var user = new UserProfile(request);

            var result = await userManager.UpdateAsync(user);

            return result;
        }

        public async Task<object> GetAllUserRole()
        {
            var result = await identityRoleRepository.GetPageListAsync(x => new { 
                Id = x.Id,
                Name = x.Name,
                NormalizedName = x.NormalizedName,
                ConcurrencyStamp = x.ConcurrencyStamp
            }, 1, 10);

            return result;
        }

        public Task<IdentityResult> ResetPassword(string userName)
        {

            // Create an instance of the SmtpClient class
            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Host = "smtp.postmarkapp.com";
                smtpClient.Credentials = new NetworkCredential("80c587fa-db26-44c9-8ca7-dc8b06d826d8", "80c587fa-db26-44c9-8ca7-dc8b06d826d8");
                //smtpClient.EnableSsl = true;
                smtpClient.Port = 587;

                try
                {
                    // Create a MailMessage object
                    MailMessage mailMessage = new MailMessage("no-replay@abcd.com", "cac0731fa0b48a89b6bb9d12bca81e59+18dbeecb78e18981c47fd75c49867646@inbound.postmarkapp.com");
                    mailMessage.IsBodyHtml = true;

                    // Set the subject and body of the email
                    mailMessage.Subject = "Test Email from SMTP in .NET";
                    mailMessage.Body = "This is a test email sent using SMTP in .NET";

                    // Send the email
                    smtpClient.Send(mailMessage);

                    Console.WriteLine("Email sent successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send email: " + ex.Message);
                }
            }
            return null;
        }

        public async Task<UserProfileDto> GetUserProfile()
        {
            (string userName, string userRole) = authenticationService.GetCurrentUser();

            var currentUser = await userManager.FindByEmailAsync(userName);

            return new UserProfileDto(currentUser);
        }

        private string GenerateAccessToken(UserProfile user, IList<string> userRoles)
        {
            var claims = new List<Claim>
            {
                new Claim("Guid", Guid.NewGuid().ToString()),
                new Claim("UserName", user.Email),
                new Claim("UserRole", userRoles.FirstOrDefault())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSetting.SecretKey));
            var accessToken = new JwtSecurityToken(
                issuer: jwtTokenSetting.Issuer,
                audience: jwtTokenSetting.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                notBefore: DateTime.Now,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        private string GenerateAccessToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSetting.SecretKey));
            var accessToken = new JwtSecurityToken(
                issuer: jwtTokenSetting.Issuer,
                audience: jwtTokenSetting.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtTokenSetting.ShortExpiryMinutes),
                notBefore: DateTime.Now,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<string> RefreshToken()
        {
            (string userName, string userRole) = authenticationService.GetCurrentUser();
            var accessToken = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];
            var refreshToken = httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return "Invalid access token or refresh token";
            }

            var user = await userManager.FindByNameAsync(userName);
            HashEntry[] hashEntries = await cacheService.GetAllHash(user.Email);

            if (user == null ||
                hashEntries.FirstOrDefault(x => x.Name == "refreshToken").Value != refreshToken ||
                DateTime.Parse(hashEntries.FirstOrDefault(x => x.Name == "expired").Value) <= DateTime.Now)
            {
                return "Invalid access token or refresh token";
            }

            var newAccessToken = GenerateAccessToken(principal.Claims.ToList());
            httpContextAccessor?.HttpContext?.Response.Cookies.Append(
                "accessToken",
                newAccessToken,
                new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Lax, Expires = DateTime.Now.AddMinutes(jwtTokenSetting.ShortExpiryMinutes) });


            var newRefreshToken = GenerateRefreshToken();
            httpContextAccessor?.HttpContext?.Response.Cookies.Append(
                "refreshToken",
                newRefreshToken,
                new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Lax, Expires = DateTime.Now.AddMinutes(jwtTokenSetting.LongExpiryMinutes) });

            HashEntry[] hashes =
            {
                new HashEntry("refreshToken", newRefreshToken),
            };
            await cacheService.SetCache(user.Email, hashes);

            return "Refresh token successful.";
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudience = jwtTokenSetting.Audience,
                ValidateIssuer = true,
                ValidIssuer = jwtTokenSetting.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSetting.SecretKey)),
                ValidateLifetime = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
    }
}

