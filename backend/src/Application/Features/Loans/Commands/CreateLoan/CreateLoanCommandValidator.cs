using FluentValidation;

namespace Biblioteca.Application.Features.Loans.Commands.CreateLoan;

public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
{
    public CreateLoanCommandValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("BookId is required.")
            .GreaterThan(0).WithMessage("BookId must be greater than 0.");
        RuleFor(x => x.BorrowerName)
            .NotEmpty().WithMessage("BorrowerName is required.");
        RuleFor(x => x.LoanDate)
            .NotEmpty().WithMessage("LoanDate is required.");
        RuleFor(x => x.DueDate)
            .NotEmpty().WithMessage("DueDate is required.")
            .GreaterThan(x => x.LoanDate).WithMessage("DueDate must be after LoanDate.");
    }
}
