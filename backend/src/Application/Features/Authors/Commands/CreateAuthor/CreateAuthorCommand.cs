using Biblioteca.Application.Features.Authors.Queries.Vms;
using MediatR;

namespace Biblioteca.Application.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommand: IRequest<AuthorVm>
{

    public string Name { get; set; }
    public string LastName { get; set; }
    public string? Biography { get; set; }
    public DateTime? BirthDate { get; set; }
    public String? Country { get; set; }



}
