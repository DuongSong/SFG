using System;
using Microsoft.AspNetCore.Identity;
using SFG.Core.Domains.Account;
using SFG.Core.Domains.Shared;

namespace SFG.Core.Services.Interfaces
{
	public interface IAccountService
	{
		Task<bool> Login(LoginRequest request);
		Task<IdentityResult> Register(AccountRegisterRequest request);
		Task<object> GetAllUserRole();
		Task<string> CreateUserRole(UserRoleRequest request);
		Task<IdentityResult> ResetPassword(string userName);
		Task<IdentityResult> ChangePassword(ChangePasswordRequest request);
		Task<UserProfileDto> GetUserProfile();
		Task<IdentityResult> UpdateUserProfile(UserProfileRequest request);
		Task<string> RefreshToken();
	}
}

