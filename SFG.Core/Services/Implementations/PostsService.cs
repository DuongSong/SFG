using System;
using Microsoft.AspNetCore.Identity;
using SFG.Core.Commons;
using SFG.Core.Domains.Posts;
using SFG.Core.Domains.Shared;
using SFG.Core.Entities.Posts;
using SFG.Core.Repositories.Interfaces;
using SFG.Core.Services.Interfaces;
using SFG.Core.Entities.Accounts;
using SFG.Core.Domains.Account;
using SFG.Core.ResponseMessages;
using SFG.Core.Kafka;

namespace SFG.Core.Services.Implementations
{
	public class PostsService : IPostsService
	{
        readonly UserManager<UserProfile> userManager;
        readonly IAuthenticationService authenticationService;
        readonly IRepository<Posts, int> postsRepository;
        readonly IRepository<Comment, int> commentRepository;
        readonly IRepository<UserLikePosts, int> userLikePostsRepository;

        public PostsService(IAuthenticationService authenticationService,
            IRepository<Posts, int> postsRepository,
            IRepository<Comment, int> commentRepository,
            IRepository<UserLikePosts, int> userLikePostsRepository,
            UserManager<UserProfile> userManager)
		{
            this.userManager = userManager;
            this.authenticationService = authenticationService;
            this.postsRepository = postsRepository;
            this.commentRepository = commentRepository;
            this.userLikePostsRepository = userLikePostsRepository;
		}

        public async Task<string> Create(PostsRequest request)
        {
            (string userName, string roleUser) = authenticationService.GetCurrentUser();

            var userProfile = await userManager.FindByEmailAsync(userName);

            var result = await postsRepository.AddEntity(new Posts
            {
                Content = request.Content,
                CreatedBy = userName,
                CreatedAt = request.CreatedAt,
                UpdatedAt = DateTime.MinValue,
                UserProfile = userProfile
            });

            return result > 0 ? PostsMessages.CREATING_POSTS_SUCCESFUL : PostsMessages.CREATING_POSTS_FAILED;
        }

        public async Task<string> Delete(int id)
        {
            var conditions = ConditionBuilder.Create<Posts>(x => x.Id == id);

            var posts = await postsRepository.GetAsyncById(conditions);
            posts.IsDeleted = true;

            var result = await postsRepository.UpdateEntity(posts);

            return result > 0 ? "Success;" : "Failed.";
        }

        public async Task<PageList<PostsDto>> GetAll(PostsQueryParam postsQueryParams)
        {
            (string userName, string roleUser) = authenticationService.GetCurrentUser();

            var conditions = ConditionBuilder.Create<Posts>(x => x.CreatedBy == userName);

            var result = await postsRepository.GetPageListAsync(
                conditions,
                x => new PostsDto(x),
                postsQueryParams.Page,
                postsQueryParams.Take,
                new string[] { "UserProfile", "UserLikePosts" }
            );

            return result;
        }

        public async Task<PostsDto> GetById(int id)
        {
            var conditions = ConditionBuilder.Create<Posts>(x => x.Id == id);

            var post = await postsRepository.GetAsyncById(conditions);

            var a = post.Comments;
            var b = post.UserLikePosts;

            var posts = await postsRepository.GetAsyncById(conditions, x => new PostsDto(x));
            return posts;
        }

        public async Task Like(int id)
        {
            (string userName, string roleUser) = authenticationService.GetCurrentUser();

            var userProfile = await userManager.FindByEmailAsync(userName);

            await userLikePostsRepository.AddEntity(new UserLikePosts { PostsId = id, UserId = userProfile.Id});
        }

        public async Task UnLike(int id)
        {
            (string userName, string roleUser) = authenticationService.GetCurrentUser();

            var userProfile = await userManager.FindByEmailAsync(userName);

            await userLikePostsRepository.DeleteEntity(new UserLikePosts { PostsId = id, UserId = userProfile.Id });
        }

        public async Task<string> Update(int id, PostsRequest request)
        {
            var conditions = ConditionBuilder.Create<Posts>(x => x.Id == id);

            var posts = await postsRepository.GetAsyncById(conditions);
            posts.Content = request.Content;
            posts.UpdatedAt = request.UpdatedAt;

            var result = await postsRepository.UpdateEntity(posts);

            return result > 0 ? "Success;" : "Failed.";
        }
    }
}

