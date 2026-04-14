namespace SmartBlog.Application.DTOs;

// DTO (Data Transfer Object) som representerar datan som klienten skickar in
// Innehåller bara de fält som användaren ska fylla i - inte Id, CreatedAt eller Summary
public record CreatePostDto(
    string Title,
    string Author,
    string Description,
    string Content
    );
