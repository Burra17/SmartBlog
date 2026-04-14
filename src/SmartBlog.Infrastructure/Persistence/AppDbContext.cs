using Microsoft.EntityFrameworkCore;
using SmartBlog.Domain.Entities;

namespace SmartBlog.Infrastructure.Persistence;

// EF Core DbContext - representerar en session mot databasen
// Innehåller DbSet:s för varje tabell vi vill komma åt
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // DbSet som ger tillgång till BlogPosts-tabellen i databasen
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();

    // Lägga till ett globalt query filter i OnModelCreating så att alla queries automatisk filtrerar bort 
    // soft deleted poster
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Globalt filter - alla queries mot BlogPost filtrerar automatiskt bort poster där IsDeleted = true
        modelBuilder.Entity<BlogPost>().HasQueryFilter(b => !b.IsDeleted);
    }
}
