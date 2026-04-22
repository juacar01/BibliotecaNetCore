using Biblioteca.Application.Features.Authors.Commands.CreateAuthor;

namespace Biblioteca.Application.Features.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommand: CreateAuthorCommand
{
    public int AuthorId { get; set; }

    public DeleteAuthorCommand(int id)
    { AuthorId = id == 0 ? throw new ArgumentNullException(nameof(id)) : id; }

}
