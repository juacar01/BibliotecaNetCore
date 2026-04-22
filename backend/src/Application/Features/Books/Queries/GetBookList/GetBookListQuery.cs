using Biblioteca.Application.Features.Books.Queries.Vms;
using MediatR;

namespace Biblioteca.Application.Features.Books.Queries.GetBookList;

public class GetBookListQuery: IRequest<IReadOnlyList<BookVm>>
{

}
