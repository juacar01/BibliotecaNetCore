using AutoMapper;
using Biblioteca.Application.Features.Books.Queries.Vms;
using Biblioteca.Application.Features.Loans.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Biblioteca.Application.Features.Loans.Queries.GetLoanList;

public class GetLoanListQueryHandler : IRequestHandler<GetLoanListQuery, IReadOnlyList<LoanVm>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLoanListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<LoanVm>> Handle(GetLoanListQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Loan, object>>>();
        includes.Add(x => x.Book);

        var loans = await _unitOfWork.Repository<Loan>().GetAsync(
            null,
            x => x.OrderByDescending(a => a.LoanDate),
            includes,
            true

            );

        var loansVm = _mapper.Map<IReadOnlyList<LoanVm>>(loans);
        return loansVm;
    }

}
