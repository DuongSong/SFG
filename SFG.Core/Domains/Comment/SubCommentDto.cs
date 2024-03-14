using System;
using SFG.Core.Entities.Posts;

namespace SFG.Core.Domains.Comment
{
	public class SubCommentDto
	{
        public int Id { get; set; }
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public SubCommentDto(SubComment subComment)
        {
            Id = subComment.Id;
            CommentId = subComment.CommentId;
            Content = subComment.Content;
            CreatedAt = subComment.CreatedAt;
            UpdatedAt = subComment.UpdatedAt;
            IsDeleted = subComment.IsDeleted;
        }
    }
}

