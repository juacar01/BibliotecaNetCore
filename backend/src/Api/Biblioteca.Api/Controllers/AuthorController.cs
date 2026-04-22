using Biblioteca.Application.Features.Authors.Commands.CreateAuthor;
using Biblioteca.Application.Features.Authors.Queries.GetAuthorList;
using Biblioteca.Application.Features.Authors.Queries.Vms;
using Biblioteca.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthorController : ControllerBase
{
    private IMediator _mediator;

    public AuthorController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("list", Name = "GetAuthors")]
    [ProducesResponseType(typeof(IReadOnlyList<AuthorVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<AuthorVm>>> GetAuthors()
    {

        var query = new GetAuthorListQuery();
        var authors = await _mediator.Send(query);

        // Implement logic to retrieve authors from the database
        return Ok(authors);
    }


    [HttpPost("create", Name = "CreateAuthor")]
    [ProducesResponseType(typeof(AuthorVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthorVm>> CreateAuthor([FromBody] CreateAuthorCommand request)
    {
        var author = await _mediator.Send(request);
        return Ok(author);
    }
}