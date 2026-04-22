using AutoMapper;
using Biblioteca.Application.Features.Books.Queries.Vms;
using Biblioteca.Application.Features.Loans.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Application.Shared.Queries;
using Biblioteca.Application.Specifications.Loans;
using Biblioteca.Domain;
using MediatR;


namespace Biblioteca.Application.Features.Loans.Queries.PaginationLoans;

public class PaginationLoansQueryHandler : IRequestHandler<PaginationLoansQuery, PaginationVm<LoanVm>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaginationLoansQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationVm<LoanVm>> Handle(PaginationLoansQuery request, CancellationToken cancellationToken)
    {

        var LoanSpecParams = new LoanSpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
            BookId = request.BookId,
            LoanDate = request.LoanDate,
            DueDate = request.DueDate
        };
 

        var spec = new LoanSpecification(LoanSpecParams);
        var loans = await _unitOfWork.Repository<Loan>().GetAllWithSpec(spec);


        var specCount = new LoanForCountingSpecification(LoanSpecParams);
        var totalItems = await _unitOfWork.Repository<Loan>().CountAsync(specCount);

        var rounded = Math.Ceiling((decimal)totalItems / request.PageSize);
        var totalPages = (int)rounded;

        var data = _mapper.Map<IReadOnlyList<Loan>, IReadOnlyList<LoanVm>>(loans);
        var loansByPage = loans.Count();

        var paginationVm = new PaginationVm<LoanVm>
        {
            Count = totalItems,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Data = data,
            PageCount = totalPages,
            ResultByPage = loansByPage
        };

        return paginationVm;
    }
}
