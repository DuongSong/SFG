using System;
namespace SFG.Core.Domains.Account
{
    public class AccountRegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string Birthday { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
    }
}

