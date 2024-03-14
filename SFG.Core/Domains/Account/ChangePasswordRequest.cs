using System;
using System.ComponentModel.DataAnnotations;

namespace SFG.Core.Domains.Account
{
	public class ChangePasswordRequest
	{
		[Required]
		public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string NewPassword { get; set; } = string.Empty;
	}
}

