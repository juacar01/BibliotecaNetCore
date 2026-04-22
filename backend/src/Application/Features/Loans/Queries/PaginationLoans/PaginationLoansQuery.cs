using Biblioteca.Application.Features.Loans.Queries.Vms;
using Biblioteca.Application.Shared.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Application.Features.Loans.Queries.PaginationLoans;

public class PaginationLoansQuery: PaginationBaseQuery, IRequest<PaginationVm<LoanVm>>
{

    public int? BookId { get; set; }
    public DateTime? LoanDate { get; set; }
    public DateTime? DueDate { get; set; }
}
