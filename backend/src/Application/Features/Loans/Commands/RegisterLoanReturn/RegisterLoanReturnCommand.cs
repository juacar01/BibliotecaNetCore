using Biblioteca.Application.Features.Loans.Commands.CreateLoan;
using Biblioteca.Application.Features.Loans.Queries.Vms;
using MediatR;

namespace Biblioteca.Application.Features.Loans.Commands.RegisterReturn;

public class RegisterLoanReturnCommand : IRequest<LoanVm>
{
    public int LoanId { get; set; } 
}
