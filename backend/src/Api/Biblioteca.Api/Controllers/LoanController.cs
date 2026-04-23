using Biblioteca.Application.Features.Authors.Commands.CreateAuthor;
using Biblioteca.Application.Features.Authors.Commands.DeleteAuthor;
using Biblioteca.Application.Features.Authors.Commands.UpdateAuthor;
using Biblioteca.Application.Features.Authors.Queries.GetAuthorById;
using Biblioteca.Application.Features.Authors.Queries.GetAuthorList;
using Biblioteca.Application.Features.Authors.Queries.PaginationAuthors;
using Biblioteca.Application.Features.Authors.Queries.Vms;
using Biblioteca.Application.Features.Loans.Commands.CreateLoan;
using Biblioteca.Application.Features.Loans.Commands.RegisterReturn;
using Biblioteca.Application.Features.Loans.Queries.GetLoanById;
using Biblioteca.Application.Features.Loans.Queries.GetLoanList;
using Biblioteca.Application.Features.Loans.Queries.PaginationLoans;
using Biblioteca.Application.Features.Loans.Queries.Vms;
using Biblioteca.Application.Shared.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LoanController : ControllerBase
{
    private IMediator _mediator;

    public LoanController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("list", Name = "GetLoans")]
    [ProducesResponseType(typeof(IReadOnlyList<LoanVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<LoanVm>>> GetLoans()
    {

        var query = new GetLoanListQuery();
        var loans = await _mediator.Send(query);

        // Implement logic to retrieve authors from the database
        return Ok(loans);
    }



    [HttpGet("pagination", Name = "PaginationLoan")]
    [ProducesResponseType(typeof(PaginationVm<LoanVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<LoanVm>>> PaginationLoan([FromQuery] PaginationLoansQuery paginationLoansQuery  )
    {
        var loans = await _mediator.Send(paginationLoansQuery);
        return Ok(loans);
    }

    [HttpPost("create", Name = "CreateLoan")]
    [ProducesResponseType(typeof(LoanVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<LoanVm>> CreateLoan([FromBody] CreateLoanCommand request)
    {
        try
        {
            var author = await _mediator.Send(request);
            return Ok(author);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    //aca


    [HttpGet("{id}", Name = "GetLoanById")]
    [ProducesResponseType(typeof(LoanVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<LoanVm>> GetLoanById(int id)
    {
        var query = new GetLoanByIdQuery(id);
        var loan = await _mediator.Send(query);
        if (loan == null) return NotFound();
        return Ok(loan);
    }


    [HttpPut("{id}/registerReturn", Name = "RegisterReturn")]
    [ProducesResponseType(typeof(LoanVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<LoanVm>> RegisterLoanReturn(int id, [FromBody] RegisterLoanReturnCommand request)
    {
        if (request == null) return BadRequest();

        request.LoanId = id; 

        var entity = await _mediator.Send(request);
        return Ok(entity);

    }


}