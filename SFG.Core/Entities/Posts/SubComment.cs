using System;
using SFG.Core.Domains.Comment;
using SFG.Core.Entities.Accounts;

namespace SFG.Core.Entities.Posts
{
	public class SubComment
	{
		public int Id { get; set; }
		public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public SubComment(SubCommentRequest request, UserProfile userProfile)
        {
            CommentId = request.CommmentId;
            CreatedBy = userProfile.UserName;
            Content = request.Content;
            CreatedAt = request.CreatedAt;
            UpdatedAt = request.UpdatedAt;
            IsDeleted = request.IsDeleted;
        }
    }
}

