using Biblioteca.Application.Features.Books.Commands.CreateBook;
using FluentValidation;

namespace Biblioteca.Application.Features.Books.Commands;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{

    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");
        RuleFor(x => x.NumberOfPages)
            .GreaterThan(0).WithMessage("Number of pages must be greater than zero.");
        RuleFor(x => x.Genre)
            .NotEmpty().WithMessage("Genre is required.")
            .MaximumLength(100).WithMessage("Genre cannot exceed 100 characters.");
        RuleFor(x => x.PublishedDate)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Published date cannot be in the future.");
        RuleFor(x => x.AuthorId)
        .NotEmpty().WithMessage("AuthorId is required.")
        .GreaterThan(0).WithMessage("AuthorId must be greater than 0.");
    }
}
