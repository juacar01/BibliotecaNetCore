using Biblioteca.Application.Features.Authors.Commands.CreateAuthor;

namespace Biblioteca.Application.Features.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommand: CreateAuthorCommand
{
    public int AuthorId { get; set; }
}
