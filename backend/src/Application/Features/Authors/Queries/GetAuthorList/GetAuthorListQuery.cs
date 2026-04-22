using Biblioteca.Application.Features.Authors.Queries.Vms;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.Features.Authors.Queries.GetAuthorList;

public class GetAuthorListQuery: IRequest<IReadOnlyList<AuthorVm>>
{

}
