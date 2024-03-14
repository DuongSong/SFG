using System;
using SFG.Core.Entities.Accounts;

namespace SFG.Core.Domains.Account
{
	public class UserProfileDto : UserProfileRequest
	{
        public string Email { get; set; } = string.Empty;

        public UserProfileDto(UserProfile userProfile)
        {
            Email = userProfile.Email;
            Avartar = userProfile.Avartar;
            FirstName = userProfile.FirstName;
            SurName = userProfile.SurName;
            Gender = userProfile.Gender;
            Birthday = userProfile.Birthday;
            Address = userProfile.Address;
            WorkAddress = userProfile.WorkAddress;
            Linked = userProfile.Linked;
            PhoneNumber = userProfile.PhoneNumber;
        }
    }
}

