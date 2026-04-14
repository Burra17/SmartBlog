using MediatR;
using SmartBlog.Application.Interfaces;

namespace SmartBlog.Application.Features.Commands.DeleteBlogPost;

// Handler som hanterar soft delete av ett blogginlägg
public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand, Unit>
{
    private readonly IBlogPostRepository _blogPostRepository;

    public DeleteBlogPostCommandHandler(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public async Task<Unit> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
    {
        // Hämta blogposten från databasen - await behövs för att invänta resultatet
        var blogPost = await _blogPostRepository.GetBlogPostByIdAsync(request.Id);

        // Om posten inte finns, kasta ett exception (fångas av ErrorHandlingMiddleware)
        if (blogPost == null)
            throw new KeyNotFoundException($"Blogpost med id '{request.Id}' hittades inte.");

        // Soft delete - sätter IsDeleted = true och sparar till databasen
        await _blogPostRepository.DeleteBlogPostAsync(blogPost);

        return Unit.Value;
    }
}
