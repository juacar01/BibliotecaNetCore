using AutoMapper;
using Biblioteca.Application.Features.Books.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Domain;
using MediatR;
using System.Linq.Expressions;


namespace Biblioteca.Application.Features.Books.Queries.GetBookList;

public class GetBookListQueryHandler: IRequestHandler<GetBookListQuery, IReadOnlyList<BookVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBookListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<BookVm>> Handle(GetBookListQuery request, CancellationToken cancellationToken)
    {
        var includes =  new List<Expression<Func<Book, object>>>();
        includes.Add(x => x.Author);

        
        var books = await _unitOfWork.Repository<Book>().GetAsync(
            null,
            x => x.OrderBy(a => a.Title),
            includes,
            true

            );

        var booksVm = _mapper.Map<IReadOnlyList<BookVm>>(books);
        return booksVm;
    }
}
