using Biblioteca.Application.Features.Authors.Queries.Vms;
using MediatR;

namespace Biblioteca.Application.Features.Authors.Queries.GetAuthorById;

public class GetAuthorByIdQuery: IRequest<AuthorVm>
{
    public int AuthorId { get; set; }

    public GetAuthorByIdQuery(int id)
    { AuthorId = id == 0 ? throw new ArgumentNullException(nameof(id)) : id; }

}
