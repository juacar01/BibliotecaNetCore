using AutoMapper;
using Biblioteca.Application.Features.Authors.Queries.Vms;
using Biblioteca.Application.Features.Books.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Domain;
using MediatR;
using System.Linq.Expressions;

namespace Biblioteca.Application.Features.Authors.Queries.GetAuthorById;

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorVm>
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthorVm> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Author, object>>>();

        var author = await _unitOfWork.Repository<Author>().GetEntityAsync(
            a => a.Id == request.AuthorId,
            includes,
            true
        );

        return _mapper.Map<AuthorVm>(author);
    }
}
