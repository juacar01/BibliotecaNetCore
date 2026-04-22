using Biblioteca.Application.Features.Authors.Queries.Vms;
using Biblioteca.Application.Shared.Queries;
using MediatR;

namespace Biblioteca.Application.Features.Authors.Queries.PaginationAuthors;

public class PaginationAuthorsQuery: PaginationBaseQuery, IRequest<PaginationVm<AuthorVm>>
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public bool? IsDeleted { get; set; }

}

