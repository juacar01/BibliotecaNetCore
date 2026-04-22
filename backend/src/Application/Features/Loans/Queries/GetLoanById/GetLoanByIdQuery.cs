using Biblioteca.Application.Features.Loans.Queries.Vms;
using MediatR;

namespace Biblioteca.Application.Features.Loans.Queries.GetLoanById;

public class GetLoanByIdQuery: IRequest<LoanVm>
{
    public int LoanId { get; set; }

    public GetLoanByIdQuery(int id)
    { LoanId = id == 0 ? throw new ArgumentNullException(nameof(id)) : id; }

}
