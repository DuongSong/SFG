using System;
using SFG.Core.Domains.Account;
using SFG.Core.Entities.Accounts;

namespace SFG.Core.Domains.Posts
{
	public class PostsDto
	{
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int LikeTotal { get; set; }
        public int CommentTotal { get; set; }
        public bool IsLike { get; set; }
        public UserProfileDto UserProfile { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public PostsDto(Entities.Posts.Posts posts)
        {
            Id = posts.Id;
            Content = posts.Content;
            LikeTotal = posts.UserLikePosts.Count;
            CommentTotal = posts.Comments.Count;
            IsLike = posts.IsLike;
            CreatedAt = posts.CreatedAt;
            UpdatedAt = posts.UpdatedAt;
            IsDeleted = posts.IsDeleted;
            UserProfile = new UserProfileDto(posts.UserProfile);
        }
	}
}

