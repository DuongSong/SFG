using System;
namespace SFG.Core.Domains.Posts
{
	public class PostsRequest
	{
		public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public PostsRequest()
		{
		}
	}
}

