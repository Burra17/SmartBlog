using SmartBlog.Application.DTOs;
using SmartBlog.Domain.Entities;

namespace SmartBlog.Application.Interfaces;

// Interface (kontrakt) för repository - definierar vilka databasoperationer som finns
// Ligger i Application-lagret så att handlern kan använda det utan att bero på Infrastructure
// Implementationen (BlogPostRepository) ligger i Infrastructure-lagret
public interface IBlogPostRepository
{
    Task CreateBlogPostAsync(BlogPost blogPost);
    Task DeleteBlogPostAsync(BlogPost blogPost);
    Task<BlogPost?> GetBlogPostByIdAsync(Guid id);
    Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();
}
