using AutoMapper;
using Biblioteca.Application.Features.Loans.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Domain;
using MediatR;
using System.Linq.Expressions;

namespace Biblioteca.Application.Features.Loans.Queries.GetLoanById;

public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, LoanVm>
{

    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public GetLoanByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LoanVm> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Loan, object>>>();
        includes.Add(b => b.Book!);
        includes.Add(x => x.Book.Author);


        var entity = await _unitOfWork.Repository<Loan>().GetEntityAsync(
            b => b.Id == request.LoanId,
            includes,
            true
        );

        return _mapper.Map<LoanVm>(entity);
    }
}
