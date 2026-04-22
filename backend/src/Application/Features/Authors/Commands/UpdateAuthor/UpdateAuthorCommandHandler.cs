using AutoMapper;
using Biblioteca.Application.Features.Authors.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.Features.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, AuthorVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthorVm> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var autorEntity = await _unitOfWork.Repository<Author>().GetByIdAsync(request.AuthorId); ;

        autorEntity.Name = request.Name;
        autorEntity.Biography = request.Biography;
        autorEntity.BirthDate = request.BirthDate;
        autorEntity.Country = request.Country;
        autorEntity.LastName = request.LastName;

        await _unitOfWork.Repository<Author>().UpdateAsync(autorEntity);

        return _mapper.Map<AuthorVm>(autorEntity);
    }
}
