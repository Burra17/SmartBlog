using AutoMapper;
using SmartBlog.Application.DTOs;
using SmartBlog.Domain.Entities;

namespace SmartBlog.Application.Mappings;

// AutoMapper-profil som definierar hur objekt ska mappas mellan typer
// Registreras automatiskt via AddAutoMapper i DependencyInjection
public class BlogPostProfile : Profile
{
    public BlogPostProfile()
    {
        // Mappar CreatePostDto -> BlogPost
        // Fält med samma namn (Title, Author, Description, Content) mappas automatiskt
        CreateMap<CreatePostDto, BlogPost>();

        // Mappar BlogPost -> GetAllPostsDto
        CreateMap<BlogPost, GetAllPostsDto>();
    }
}
