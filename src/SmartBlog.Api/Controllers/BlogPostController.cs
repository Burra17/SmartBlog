using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartBlog.Application.DTOs;
using SmartBlog.Application.Features.Commands.CreateBlogPost;
using SmartBlog.Application.Features.Commands.DeleteBlogPost;
using SmartBlog.Application.Features.Queries.GetAllBlogPosts;

namespace SmartBlog.Api.Controllers
{
    // Controller som tar emot HTTP-anrop för blogginlägg
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostController : ControllerBase
    {
        // MediatR används för att skicka commands/queries till rätt handler
        private readonly IMediator _mediator;

        public BlogPostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/blogpost - Skapar ett nytt blogginlägg
        // Tar emot en CreatePostDto från request body, skickar den som ett command via MediatR
        // och returnerar den AI-genererade sammanfattningen
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost(CreatePostDto createPostDto)
        {
            var result = await _mediator.Send(new CreateBlogPostCommand(createPostDto));
            return Ok(result);
        }

        // DELETE api/blogpost/{id} - Soft delete av ett blogginlägg
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(Guid id)
        {
            await _mediator.Send(new DeleteBlogPostCommand(id));
            return NoContent();
        }

        // GET api/blogpost - hämta alla blogpost
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var result = await _mediator.Send(new GetAllBlogPostsQuery());
            return Ok(result);
        }
    }
}
