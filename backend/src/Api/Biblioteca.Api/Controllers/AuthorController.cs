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
using Swashbuckle.AspNetCore.Annotations;
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
    [SwaggerOperation(
        Summary = "Retorna el listado completo de Autores",
        Description = "Retorna el listado completo de Autores"
    )]
    [ProducesResponseType(typeof(IReadOnlyList<AuthorVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<AuthorVm>>> GetAuthors()
    {

        var query = new GetAuthorListQuery();
        var authors = await _mediator.Send(query);

        // Implement logic to retrieve authors from the database
        return Ok(authors);
    }



    [HttpGet("pagination", Name = "PaginationAuthor")]
    [SwaggerOperation(
        Summary = "Retorna los datos de préstamos",
        Description = "Retorna un listado de prestamos mediante paginacion. ej: pageindex=x, search=dato"
    )]
    [ProducesResponseType(typeof(PaginationVm<AuthorVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<AuthorVm>>> PaginationAuthor([FromQuery] PaginationAuthorsQuery paginationAuthorsQuery  )
    {
        var books = await _mediator.Send(paginationAuthorsQuery);
        return Ok(books);
    }

    [HttpPost("create", Name = "CreateAuthor")]
    [SwaggerOperation(
        Summary = "Registra los datos de un nuevo Autor",
        Description = "Registra los datos de un Nuevo Autor"
    )]
    [ProducesResponseType(typeof(AuthorVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthorVm>> CreateAuthor([FromBody] CreateAuthorCommand request)
    {
        var author = await _mediator.Send(request);
        return Ok(author);
    }

    //aca


    [HttpGet("{id}", Name = "GetAuthorById")]
    [SwaggerOperation(
        Summary = "Retorna los datos de un Autor por su ID",
        Description = "Retorna los datos de un Autor por su Id"
    )]
    [ProducesResponseType(typeof(AuthorVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthorVm>> GetAuthorById(int id)
    {
        var query = new GetAuthorByIdQuery(id);
        var book = await _mediator.Send(query);
        if (book == null) return NotFound();
        return Ok(book);
    }

    [HttpDelete("{id}", Name = "DeleteAuthor")]
    [SwaggerOperation(
        Summary = "Elimina un Autor",
        Description = "Realmente no elimina el autor, sino que establece si propiedad IsDeleted a true"
    )]
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

        request.AuthorId = id; 

        var author = await _mediator.Send(request);
        return Ok(author);

    }


}