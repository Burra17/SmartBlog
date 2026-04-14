using FluentValidation;

namespace SmartBlog.Application.Features.Commands.CreateBlogPost;

// Validator som validerar CreateBlogPostCommand innan handlern körs
// Plockas upp automatiskt av ValidationBehavior i MediatR-pipelinen
public class CreateBlogPostCommandValidator : AbstractValidator<CreateBlogPostCommand>
{
    public CreateBlogPostCommandValidator()
    {
        // Validera att alla obligatoriska fält i DTO:n är ifyllda
        RuleFor(x => x.createPostDto.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(x => x.createPostDto.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(x => x.createPostDto.Author)
            .NotEmpty().WithMessage("Author is required.");

        RuleFor(x => x.createPostDto.Content)
            .NotEmpty().WithMessage("Content is required.");
    }
}
