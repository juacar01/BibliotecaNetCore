using Biblioteca.Application.Features.Loans.Queries.Vms;
using MediatR;

namespace Biblioteca.Application.Features.Loans.Queries.GetLoanList;

public class GetLoanListQuery: IRequest<IReadOnlyList<LoanVm>>
{
}
