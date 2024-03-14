using System;
namespace SFG.Core.Domains.Comment
{
	public class SubCommentRequest
	{
		public int CommmentId { get; set; }
		public string Content { get; set; } = string.Empty;
		public string CreatedBy { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool IsDeleted { get; set; }
	}
}

