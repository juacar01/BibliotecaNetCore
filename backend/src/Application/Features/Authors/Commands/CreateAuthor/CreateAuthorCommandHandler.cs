using AutoMapper;
using Biblioteca.Application.Features.Authors.Queries.Vms;
using Biblioteca.Application.Features.Books.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, AuthorVm>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAuthorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<AuthorVm> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorEntity = _mapper.Map<Author>(request);
        await _unitOfWork.Repository<Author>().AddAsync(authorEntity);

        return _mapper.Map<AuthorVm>(authorEntity);
    }

}
