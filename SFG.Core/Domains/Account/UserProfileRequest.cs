using System;
namespace SFG.Core.Domains.Account
{
	public class UserProfileRequest
	{
		public string Avartar { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Birthday { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string WorkAddress { get; set; } = string.Empty;
        public string Linked { get; set; } = string.Empty;
    }
}

