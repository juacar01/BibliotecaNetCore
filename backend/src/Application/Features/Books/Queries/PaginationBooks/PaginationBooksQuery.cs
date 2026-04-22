using Biblioteca.Application.Features.Books.Queries.Vms;
using Biblioteca.Application.Shared.Queries;
using MediatR;

namespace Biblioteca.Application.Features.Books.Queries.PaginationBooks;

public class PaginationAuthorsQuery: PaginationBaseQuery, IRequest<PaginationVm<BookVm>> 
{
    public string? Title { get; set; }
    public int? AuthorId { get; set; }   
    public bool? IsDeleted { get; set; }
}
