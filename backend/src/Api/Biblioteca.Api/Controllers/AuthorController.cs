using Biblioteca.Application.Features.Authors.Commands.CreateAuthor;
using Biblioteca.Application.Features.Authors.Commands.DeleteAuthor;
using Biblioteca.Application.Features.Authors.Commands.UpdateAuthor;
using Biblioteca.Application.Features.Authors.Queries.GetAuthorById;
using Biblioteca.Application.Features.Authors.Queries.GetAuthorList;
using Biblioteca.Application.Features.Authors.Queries.PaginationAuthors;
using Biblioteca.Application.Features.Authors.Queries.Vms;
using Biblioteca.Application.Shared.Queries;
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



    [HttpGet("pagination", Name = "PaginationAuthor")]
    [ProducesResponseType(typeof(PaginationVm<AuthorVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<AuthorVm>>> PaginationAuthor([FromQuery] PaginationAuthorsQuery paginationAuthorsQuery  )
    {
        var books = await _mediator.Send(paginationAuthorsQuery);
        return Ok(books);
    }

    [HttpPost("create", Name = "CreateAuthor")]
    [ProducesResponseType(typeof(AuthorVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthorVm>> CreateAuthor([FromBody] CreateAuthorCommand request)
    {
        var author = await _mediator.Send(request);
        return Ok(author);
    }

    //aca


    [HttpGet("{id}", Name = "GetAuthorById")]
    [ProducesResponseType(typeof(AuthorVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthorVm>> GetAuthorById(int id)
    {
        var query = new GetAuthorByIdQuery(id);
        var book = await _mediator.Send(query);
        if (book == null) return NotFound();
        return Ok(book);
    }

    [HttpDelete("{id}", Name = "DeleteAuthor")]
    [ProducesResponseType(typeof(AuthorVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthorVm>> DeleteAuthor(int id)
    {
        var command = new DeleteAuthorCommand(id);
        var book = await _mediator.Send(command);
        if (book == null) return NotFound();
        return Ok(book);
    }

    [HttpPut("{id}", Name = "UpdateAuthor")]
    [ProducesResponseType(typeof(AuthorVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthorVm>> UpdateAuthor(int id, [FromBody] UpdateAuthorCommand request)
    {
        if (request == null) return BadRequest();

        request.AuthorId = id; // Aseguramos que el ID del autor a actualizar se establezca correctamente

        var author = await _mediator.Send(request);
        return Ok(author);

    }


}