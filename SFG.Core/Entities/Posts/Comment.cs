
using SFG.Core.Domains.Comment;
using SFG.Core.Entities.Accounts;

namespace SFG.Core.Entities.Posts
{
	public class Comment
	{
        public int Id { get; set; }
		public string Content { get; set; } = string.Empty;
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<SubComment> SubComments { get; set; } = new List<SubComment>();

        public Comment(CommentRequest request, UserProfile userProfile)
        {
            Content = request.Content;
            CreatedBy = userProfile.UserName;
            CreatedAt = request.CreatedAt;
            UpdatedAt = request.UpdatedAt;
            IsDeleted = request.IsDeleted;
        }
    }
}

