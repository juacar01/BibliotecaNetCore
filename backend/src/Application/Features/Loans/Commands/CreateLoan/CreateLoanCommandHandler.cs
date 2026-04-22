using AutoMapper;
using Biblioteca.Application.Exceptions;
using Biblioteca.Application.Features.Loans.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Domain;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;
using System.Net;

namespace Biblioteca.Application.Features.Loans.Commands.CreateLoan;

public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, LoanVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateLoanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LoanVm> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        var loanExists = await _unitOfWork.Repository<Loan>()
            .AnyAsync(l => l.BookId == request.BookId && l.ReturnDate == null);

        if (loanExists)
        {
            throw new ConflictException("Book is already loaned out");
        }

        var bookExists = await _unitOfWork.Repository<Book>()
            .AnyAsync(l => l.Id == request.BookId);

        if (!bookExists)
        {
            throw new BadRequestException("Book does not exist");
        }

        if(request.DueDate <= request.LoanDate)
        {
            throw new BadRequestException("Due date must be after loan date");
        }

        var entity = _mapper.Map<Loan>(request);
        await _unitOfWork.Repository<Loan>().AddAsync(entity);
        await _unitOfWork.Complete();
        // Cargar con Include igual que GetLoanByIdQueryHandler
        var includes = new List<Expression<Func<Loan, object>>>();
        includes.Add(l => l.Book!);

        var loanWithBook = await _unitOfWork.Repository<Loan>().GetEntityAsync(
            l => l.Id == entity.Id,
            includes,
            true
        );

        return _mapper.Map<LoanVm>(loanWithBook);
    }
}
