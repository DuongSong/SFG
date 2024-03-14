using Microsoft.AspNetCore.Identity;
using SFG.Core.Domains.Comment;
using SFG.Core.Entities.Accounts;
using SFG.Core.Entities.Posts;
using SFG.Core.Repositories.Interfaces;
using SFG.Core.Services.Interfaces;

namespace SFG.Core.Services.Implementations
{
    public class CommentService : ICommentService
	{
        readonly UserManager<UserProfile> userManager;
        readonly IAuthenticationService authenticationService;
        readonly IRepository<Comment, int> commentRepository;
        readonly IRepository<SubComment, int> subCommentRepository;

		public CommentService(UserManager<UserProfile> userManager, IAuthenticationService authenticationService,
            IRepository<Comment, int> commentRepository, IRepository<SubComment, int> subCommentRepository)
		{
            this.userManager = userManager;
            this.authenticationService = authenticationService;
            this.commentRepository = commentRepository;
            this.subCommentRepository = subCommentRepository;
		}

        public async Task<string> Create(CommentRequest request)
        {
            (string userName, string userRole) = authenticationService.GetCurrentUser();

            var userProfile = await userManager.FindByEmailAsync(userName);

            var result = await commentRepository.AddEntity(new Comment(request, userProfile));

            return result != 0 ? "Success." : "Failed.";
        }

        public async Task<string> Create(SubCommentRequest request)
        {
            (string userName, string userRole) = authenticationService.GetCurrentUser();

            var userProfile = await userManager.FindByEmailAsync(userName);

            var result = await subCommentRepository.AddEntity(new SubComment(request, userProfile));

            return result != 0 ? "Success." : "Failed.";
        }

        public Task<CommentDto> GetAll(CommentQueryParam queryParam)
        {
            throw new NotImplementedException();
        }

        public Task<string> Update(int id, CommentRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<string> Update(int id, SubCommentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

