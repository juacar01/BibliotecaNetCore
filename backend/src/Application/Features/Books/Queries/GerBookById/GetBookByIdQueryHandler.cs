using AutoMapper;
using Biblioteca.Application.Features.Books.Queries.GetBookById;
using Biblioteca.Application.Features.Books.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Domain;
using MediatR;
using System.Linq.Expressions;

namespace Biblioteca.Application.Features.Books.Queries.GerBookById;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookVm>
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public GetBookByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<BookVm> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Book, object>>> ();

        var book = await _unitOfWork.Repository<Book>().GetEntityAsync(
            b => b.Id == request.BookId,
            includes,
            true
        );

        return _mapper.Map<BookVm>(book);
    }
}
