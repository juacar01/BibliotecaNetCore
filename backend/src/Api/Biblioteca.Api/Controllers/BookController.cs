using Biblioteca.Application.Features.Books.Commands.CreateBook;
using Biblioteca.Application.Features.Books.Queries.GetBookList;
using Biblioteca.Application.Features.Books.Queries.Vms;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Biblioteca.Api.Services;
using Biblioteca.Application.Shared.Queries;
using Biblioteca.Application.Features.Books.Queries.PaginationBooks;
using Biblioteca.Application.Features.Books.Queries.GetBookById;
using Biblioteca.Application.Features.Books.Commands.DeleteBook;
using Biblioteca.Application.Features.Books.Commands.UpdateBook;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BookController : ControllerBase
{
    private IMediator _mediator;
    private readonly IFileStorageService _fileStorageService;

    public BookController(IMediator mediator, IFileStorageService fileStorageService)
    {
        _mediator = mediator;
        _fileStorageService = fileStorageService;
    }


    [HttpGet("list", Name = "GetBooks")]
    [ProducesResponseType(typeof(IReadOnlyList<BookVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<BookVm>>> GetBooks()
    {

        var query = new GetBookListQuery();
        var books = await _mediator.Send(query);

        // Implement logic to retrieve books from the database
        return Ok(books);
    }

    [HttpGet("pagination", Name = "PaginationBook")]
    [ProducesResponseType(typeof(PaginationVm<BookVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<BookVm>>> PaginationBook([FromQuery] PaginationAuthorsQuery paginationBooksQuery)
    { 
        var books = await _mediator.Send(paginationBooksQuery);
        return Ok(books);
    }



    [HttpPost("create", Name = "CreateBook")]
    [ProducesResponseType(typeof(BookVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BookVm>> CreateBook([FromForm] CreateBookCommand request)
    {
        if (request == null) return BadRequest();

        if (request.Imagen != null)
        {
            // Validaciones básicas
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var ext = Path.GetExtension(request.Imagen.FileName).ToLowerInvariant();
            if (!allowed.Contains(ext)) return BadRequest("Formato de imagen no permitido.");
            if (request.Imagen.Length > 5 * 1024 * 1024) return BadRequest("Archivo demasiado grande.");

            var relativePath = await _fileStorageService.SaveUploadAsync(request.Imagen, "Uploads/books");
            request.CoverImagePath = relativePath; // guardar ruta relativa en la entidad/comando
        }
        else
        {
            request.CoverImagePath = "/Uploads/default.jpg";
        }


        var book = await _mediator.Send(request);
        return Ok(book);
    }


    [HttpGet("{id}", Name = "GetBookById")]
    [ProducesResponseType(typeof(BookVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BookVm>> GetBookById(int id)
    {         
        var query = new GetBookByIdQuery(id);
        var book = await _mediator.Send(query);
        if (book == null) return NotFound();
        return Ok(book);
    }

    [HttpDelete("{id}", Name = "DeleteBook")]
    [ProducesResponseType(typeof(BookVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BookVm>> DeleteBook(int id)
    {
        var command = new DeleteBookCommand(id);
        var book = await _mediator.Send(command);
        if (book == null) return NotFound();
        return Ok(book);
    }

    [HttpPut("{id}", Name = "UpdateBook")]
    [ProducesResponseType(typeof(BookVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BookVm>> UpdateBook(int id, [FromForm] UpdateBookCommand request)
    {
        if (request == null) return BadRequest();

        if (request.Imagen != null)
        {
            // Validaciones básicas
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var ext = Path.GetExtension(request.Imagen.FileName).ToLowerInvariant();
            if (!allowed.Contains(ext)) return BadRequest("Formato de imagen no permitido.");
            if (request.Imagen.Length > 5 * 1024 * 1024) return BadRequest("Archivo demasiado grande.");

            var relativePath = await _fileStorageService.SaveUploadAsync(request.Imagen, "Uploads/books");
            request.CoverImagePath = relativePath; // guardar ruta relativa en la entidad/comando
        }

        request.BookId = id; // Aseguramos que el ID del libro a actualizar se establezca correctamente


        var book = await _mediator.Send(request);
        return Ok(book);

    }
}