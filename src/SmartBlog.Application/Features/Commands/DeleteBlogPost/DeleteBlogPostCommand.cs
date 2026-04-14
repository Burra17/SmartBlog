using MediatR;

namespace SmartBlog.Application.Features.Commands.DeleteBlogPost;

// Radera baserat på id, Skicka inget returvärde bara kolla så det fukar
public record DeleteBlogPostCommand(Guid Id) : IRequest<Unit>;
