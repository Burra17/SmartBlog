using Microsoft.EntityFrameworkCore;
using SmartBlog.Application.Interfaces;
using SmartBlog.Domain.Entities;
using SmartBlog.Infrastructure.Persistence;

namespace SmartBlog.Infrastructure.Repositories;

// Implementation av IBlogPostRepository - hanterar databasoperationer för blogginlägg
// Använder EF Core (AppDbContext) för att prata med databasen
public class BlogPostRepository : IBlogPostRepository
{
    private readonly AppDbContext _appDbContext;

    public BlogPostRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    // Lägg till en ny blogpost i databasen och spara ändringarna
    public async Task CreateBlogPostAsync(BlogPost blogPost)
    {
        await _appDbContext.AddAsync(blogPost);
        await _appDbContext.SaveChangesAsync();
    }

    // Soft delete - sätt IsDeleted till true och spara ändringen i databasen
    // Blogposten är redan trackad av EF Core så SaveChangesAsync vet vad som ändrats
    public async Task DeleteBlogPostAsync(BlogPost blogPost)
    {
        blogPost.IsDeleted = true;
        await _appDbContext.SaveChangesAsync();
    }


    // Hämta alla blogposts från databasen
    public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
    {
        return await _appDbContext.BlogPosts.ToListAsync();
    }

    // Hämta en blogpost från databasen med hjälp av id
    // Returnerar null om posten inte finns
    public async Task<BlogPost?> GetBlogPostByIdAsync(Guid id)
    {
        return await _appDbContext.BlogPosts.FindAsync(id);
    }

}
