namespace SmartBlog.Application.DTOs;

public record GetAllPostsDto(
    Guid Id,
    string Title,
    string Author,
    string Description,
    string Content,
    string Summary,
    DateTime CreatedAt
    );
