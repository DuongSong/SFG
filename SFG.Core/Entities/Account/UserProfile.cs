using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SFG.Core.Domains.Account;

namespace SFG.Core.Entities.Accounts
{
    public class UserProfile : IdentityUser
	{
		[MaxLength(256)]
		public string FirstName { get; set; } = string.Empty;
        [MaxLength(256)]
        public string SurName { get; set; } = string.Empty;
        public string Birthday { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string WorkAddress { get; set; } = string.Empty;
        public string Linked { get; set; } = string.Empty;
        public string Avartar { get; set; } = string.Empty;
		
        public UserProfile() {}

		public UserProfile(AccountRegisterRequest userRequest)
		{
			FirstName = userRequest.FirstName;
			SurName = userRequest.SurName;
			Birthday = userRequest.Birthday;
			Gender = userRequest.Gender;
			UserName = userRequest.Email;
			NormalizedUserName = userRequest.Email;
            Email = userRequest.Email;
			NormalizedEmail = userRequest.Email;
        }

        public UserProfile(UserProfileRequest userRequest)
        {
            Avartar = userRequest.Avartar;
            FirstName = userRequest.FirstName;
            SurName = userRequest.SurName;
            PhoneNumber = userRequest.PhoneNumber;
            Birthday = userRequest.Birthday;
            Gender = userRequest.Gender;
            Address = userRequest.Address;
            WorkAddress = userRequest.WorkAddress;
            Linked = userRequest.Linked;
        }
    }
}

