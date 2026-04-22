using AutoMapper;
using Biblioteca.Application.Features.Books.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Application.Shared.Queries;
using Biblioteca.Application.Specifications.Books;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.Features.Books.Queries.PaginationBooks;

public class PaginationBooksQueryHandler : IRequestHandler<PaginationBooksQuery, PaginationVm<BookVm>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaginationBooksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<PaginationVm<BookVm>> Handle(PaginationBooksQuery request, CancellationToken cancellationToken)
    {
        var BookEspecParams = new BookSpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Title = request.Title,
            Sort = request.Sort,
            AuthorId = request.AuthorId
        };

        var spec = new BookSpecification(BookEspecParams);
        var books = await _unitOfWork.Repository<Book>().GetAllWithSpec(spec);

        var specCount = new BookForCountingSpecification(BookEspecParams);
        var totalItems = await _unitOfWork.Repository<Book>().CountAsync(specCount);

        var rounded = Math.Ceiling((decimal)totalItems / request.PageSize);
        var totalPages = (int)rounded;

        var data = _mapper.Map<IReadOnlyList<Book>, IReadOnlyList<BookVm>>(books);
        var booksByPage = books.Count();

        var paginationVm = new PaginationVm<BookVm>
        {
            Count = totalItems,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Data = data,
            PageCount = totalPages,
            ResultByPage = booksByPage
        };

        return paginationVm;
    }
}
