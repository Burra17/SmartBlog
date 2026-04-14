using AutoMapper;
using MediatR;
using SmartBlog.Application.DTOs;
using SmartBlog.Application.Interfaces;
using SmartBlog.Domain.Entities;

namespace SmartBlog.Application.Features.Queries.GetAllBlogPosts;

public class GetAllBlogPostsQueryHandler : IRequestHandler<GetAllBlogPostsQuery, IEnumerable<GetAllPostsDto>>
{
    private readonly IMapper _mapper;
    private readonly IBlogPostRepository _blogPostRepository;

    public GetAllBlogPostsQueryHandler(IMapper mapper, IBlogPostRepository blogPostRepository)
    {
        _mapper = mapper;
        _blogPostRepository = blogPostRepository;
    }

    public async Task<IEnumerable<GetAllPostsDto>> Handle(GetAllBlogPostsQuery request, CancellationToken cancellationToken)
    {
        // 1. Hämta alla inlägg från databasen (Repositoryt sköter jobbet)
        var blogPosts = await _blogPostRepository.GetAllBlogPostsAsync();

        // 2. Mappa listan med entiteter TILL en lista med DTO:er
        var dtos = _mapper.Map<List<GetAllPostsDto>>(blogPosts);

        // 3. Returnera DTO-listan till controllern
        return dtos;
    }
}
