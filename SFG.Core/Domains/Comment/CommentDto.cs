using System;
using SFG.Core.Entities.Posts;

namespace SFG.Core.Domains.Comment
{
	public class CommentDto
	{
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public List<SubComment> SubComments { get; set; } = new List<SubComment>();

        public CommentDto(SFG.Core.Entities.Posts.Comment comment)
        {
            Id = comment.Id;
            Content = comment.Content;
            CreatedAt = comment.CreatedAt;
            UpdatedAt = comment.UpdatedAt;
            IsDeleted = comment.IsDeleted;
            SubComments = comment.SubComments.ToList();
        }
    }
}

