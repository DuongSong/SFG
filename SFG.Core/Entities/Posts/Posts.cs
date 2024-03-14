using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SFG.Core.Domains.Posts;
using SFG.Core.Entities.Accounts;

namespace SFG.Core.Entities.Posts
{
	public class Posts
	{
		public int Id { get; set; }
		public string Content { get; set; } = string.Empty;
		public bool IsLike { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool IsDeleted { get; set; }
		public virtual UserProfile UserProfile { get;set;}
        public virtual ICollection<UserLikePosts> UserLikePosts { get; set; } = new List<UserLikePosts>();
		public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

		public Posts() { }
	}
}

