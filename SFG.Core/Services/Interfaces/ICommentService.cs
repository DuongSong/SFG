using System;
using SFG.Core.Domains.Comment;

namespace SFG.Core.Services.Interfaces
{
	public interface ICommentService
	{
		Task<CommentDto> GetAll(CommentQueryParam queryParam);
		Task<string> Create(CommentRequest request);
		Task<string> Create(SubCommentRequest request);
		Task<string> Update(int id, CommentRequest request);
		Task<string> Update(int id, SubCommentRequest request);
	}
}

