using System;
namespace SFG.Core.Services.Interfaces
{
	public interface IAuthenticationService
	{
        bool IsAuthenticated { get; }

        (string userName, string userRole) GetCurrentUser();

        Task<bool> CheckRefreshToken();
    }
}

