using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFG.Core.Domains.Posts;
using SFG.Core.Services.Interfaces;

namespace SFG.WebApi.Controllers
{
	[Authorize]
	[ApiController]
    [Route("sfg/posts")]
    public class PostsController : ControllerBase
	{
		readonly IPostsService postsService;

		public PostsController(IPostsService postsService)
		{
			this.postsService = postsService;
		}

		[HttpGet("get-all")]
		public async Task<IActionResult> GetAll([FromQuery] PostsQueryParam postsQueryParam)
		{
			var result = await postsService.GetAll(postsQueryParam);
			return Ok(result);
		}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = await postsService.GetById(id);
            return Ok(result);
        }

        [HttpPost("create-posts")]
		public async Task<IActionResult> Create([FromBody] PostsRequest request)
		{
			var result = await postsService.Create(request);
			return Ok(result);
		}

		[HttpPost("update-posts/{id}")]
		public async Task<IActionResult> Update([FromRoute]int id, PostsRequest request)
		{
			var result = await postsService.Update(id, request);
			return Ok(result);
		}

		[HttpPost("delete-posts/{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var result = await postsService.Delete(id);
			return Ok(result);
		}

		[HttpPut("like/{id}")]
		public async Task<IActionResult> Like([FromRoute] int id)
		{
			await postsService.Like(id);
            return Ok();
		}

        [HttpPut("unlike/{id}")]
        public async Task<IActionResult> UnLike([FromRoute] int id)
        {
            await postsService.UnLike(id);
            return Ok();
        }
    }
}

