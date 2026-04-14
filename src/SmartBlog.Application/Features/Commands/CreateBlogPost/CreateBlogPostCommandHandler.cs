using AutoMapper;
using MediatR;
using SmartBlog.Application.Interfaces;
using SmartBlog.Domain.Entities;

namespace SmartBlog.Application.Features.Commands.CreateBlogPost
{
    // Handler som innehåller affärslogiken för att skapa ett blogginlägg
    // Anropas automatiskt av MediatR när en CreateBlogPostCommand skickas
    public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, string>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IMapper _mapper;
        private readonly IOpenAiService _openAiService;

        // Alla beroenden injiceras via konstruktorn (Dependency Injection)
        public CreateBlogPostCommandHandler(IBlogPostRepository blogPostRepository, IMapper mapper, IOpenAiService openAiService)
        {
            _blogPostRepository = blogPostRepository;
            _mapper = mapper;
            _openAiService = openAiService;
        }

        async Task<string> IRequestHandler<CreateBlogPostCommand, string>.Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
        {
            // Steg 1: Mappa DTO:n till en BlogPost-entitet med AutoMapper
            var blogPost = _mapper.Map<BlogPost>(request.createPostDto);

            // Steg 2: Anropa OpenAI för att generera en sammanfattning av innehållet
            blogPost.Summary = await _openAiService.GenerateSummaryAsync(blogPost.Content, cancellationToken);

            // Steg 3: Spara blogposten med summary i databasen via repository
            await _blogPostRepository.CreateBlogPostAsync(blogPost);

            // Steg 4: Returnera den genererade sammanfattningen till controllern
            return blogPost.Summary;
        }
    }
}
