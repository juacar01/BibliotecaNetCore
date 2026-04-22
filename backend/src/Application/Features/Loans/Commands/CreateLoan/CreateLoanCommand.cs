using Biblioteca.Application.Features.Books.Queries.Vms;
using Biblioteca.Application.Features.Loans.Queries.Vms;
using MediatR;

namespace Biblioteca.Application.Features.Loans.Commands.CreateLoan;

public class CreateLoanCommand: IRequest<LoanVm>
{

    public int Id { get; set; }
    public int BookId { get; set; }
    public String BorrowerName { get; set; }
    public DateTime LoanDate { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; } = DateTime.Now.AddDays(5);
    public DateTime? ReturnDate { get; set; }
    public virtual BookVm? Book { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
