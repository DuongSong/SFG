using System;
using SFG.Core.Domains.Posts;
using SFG.Core.Domains.Shared;

namespace SFG.Core.Services.Interfaces
{
	public interface IPostsService
	{
		Task<PageList<PostsDto>> GetAll(PostsQueryParam postsQueryParam);
		Task<PostsDto> GetById(int id);
		Task<string> Create(PostsRequest request);
		Task<string> Update(int id, PostsRequest request);
		Task<string> Delete(int id);
		Task Like(int id);
		Task UnLike(int id);
	}
}

