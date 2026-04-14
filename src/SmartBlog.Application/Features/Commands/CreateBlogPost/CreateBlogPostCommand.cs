using MediatR;
using SmartBlog.Application.DTOs;

namespace SmartBlog.Application.Features.Commands.CreateBlogPost;

// Command som representerar en begäran om att skapa ett nytt blogginlägg
// Innehåller DTO:n med data från klienten
// IRequest<string> betyder att handlern returnerar en string (AI-genererad summary)
public record CreateBlogPostCommand(CreatePostDto createPostDto) : IRequest<string>;
