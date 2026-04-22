using Biblioteca.Application.Features.Books.Commands.CreateBook;

namespace Biblioteca.Application.Features.Books.Commands.DeleteBook;

public class DeleteBookCommand: CreateBookCommand
{
    public int BookId { get; set; }

    public DeleteBookCommand(int id)
    { BookId = id == 0 ? throw new ArgumentNullException(nameof(id)) : id; }
}
