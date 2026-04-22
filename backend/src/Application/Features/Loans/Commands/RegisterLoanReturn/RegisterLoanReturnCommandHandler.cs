using AutoMapper;
using Biblioteca.Application.Features.Loans.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.Features.Loans.Commands.RegisterReturn;

public class RegisterLoanReturnCommandHandler : IRequestHandler<RegisterLoanReturnCommand, LoanVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterLoanReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<LoanVm> Handle(RegisterLoanReturnCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<Loan>().GetByIdAsync(request.LoanId); ;

        entity.ReturnDate = DateTime.UtcNow;

        await _unitOfWork.Repository<Loan>().UpdateAsync(entity);

        return _mapper.Map<LoanVm>(entity);
    }
}
