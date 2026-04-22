using Biblioteca.Application.Features.Books.Queries.Vms;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Application.Features.Books.Commands.CreateBook;

public class CreateBookCommand: IRequest<BookVm>
{
    public string Title { get; set; }
    public int NumberOfPages { get; set; }
    public string Genre { get; set; }
    public DateTime? PublishedDate { get; set; }
    public string? CoverImagePath { get; set; }
    public int AuthorId { get; set; }

    public IFormFile? Imagen { get; set; } = null;
}
