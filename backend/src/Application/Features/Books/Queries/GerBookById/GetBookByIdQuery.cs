using Biblioteca.Application.Features.Books.Queries.Vms;
using MediatR;

namespace Biblioteca.Application.Features.Books.Queries.GetBookById;

public class GetBookByIdQuery : IRequest<BookVm>
{
    public int BookId { get; set; }

    public GetBookByIdQuery(int id)
    { BookId = id == 0 ? throw new ArgumentNullException(nameof(id)) : id; }

}
