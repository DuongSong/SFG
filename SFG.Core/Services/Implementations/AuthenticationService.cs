using System.Text.Json;
using SFG.Core.Services.Interfaces;

namespace SFG.Core.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
	{
		readonly IHttpContextAccessor httpContextAccessor;
		readonly ICacheService cacheService;

		public AuthenticationService(IHttpContextAccessor httpContextAccessor, ICacheService cacheService)
		{
			this.httpContextAccessor = httpContextAccessor;
			this.cacheService = cacheService;
		}

        public bool IsAuthenticated => httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public async Task<bool> CheckRefreshToken()
        {
            (string userName, string userRole) = GetCurrentUser();

			var data = await cacheService.GetCache(userName, "expired");
			DateTime expired = DateTime.Parse(data);

			if(expired < DateTime.Now)
			{
				return false;
			}
			return true;
        }

        public (string userName, string userRole) GetCurrentUser()
        {
			var userName = httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type == "UserName").FirstOrDefault()?.Value ?? string.Empty;

			var userRole = httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type == "UserRole").FirstOrDefault()?.Value ?? string.Empty;

			return (userName, userRole);
        }
    }
}


