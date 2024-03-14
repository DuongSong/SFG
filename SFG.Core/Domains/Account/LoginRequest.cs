using System;
using System.ComponentModel.DataAnnotations;

namespace SFG.Core.Domains.Account
{
	public class LoginRequest
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
	}
}

