namespace SmartBlog.Domain.Entities;

// Domänentitet som representerar ett blogginlägg i databasen
// Alla properties mappas till kolumner i tabellen BlogPosts
public class BlogPost
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Sammanfattning som genereras av OpenAI
    public string Summary { get; set; } = string.Empty;

    // IsDeleted bool för att kunna göra en softdelete
    public bool IsDeleted { get; set; } = false;
}
