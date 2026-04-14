using MediatR;
using SmartBlog.Application.DTOs;

namespace SmartBlog.Application.Features.Queries.GetAllBlogPosts;

public record GetAllBlogPostsQuery : IRequest<IEnumerable<GetAllPostsDto>>;
