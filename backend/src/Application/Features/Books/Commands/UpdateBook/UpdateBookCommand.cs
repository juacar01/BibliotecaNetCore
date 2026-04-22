using Biblioteca.Application.Features.Books.Commands.CreateBook;
using Biblioteca.Application.Features.Books.Queries.Vms;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Application.Features.Books.Commands.UpdateBook;

public class UpdateBookCommand : CreateBookCommand
{
    public int BookId { get; set; }

}