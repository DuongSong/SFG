using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SFG.Core.Entities.Posts
{
	public class UserLikePosts
	{
		public string UserId { get; set; } = string.Empty;
		public int PostsId { get; set; }
	}
}

