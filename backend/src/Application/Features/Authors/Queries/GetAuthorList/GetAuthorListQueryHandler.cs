using AutoMapper;
using Biblioteca.Application.Features.Authors.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Domain;
using MediatR;
using System.Linq.Expressions;


namespace Biblioteca.Application.Features.Authors.Queries.GetAuthorList;

public class GetAuthorListQueryHandler: IRequestHandler<GetAuthorListQuery, IReadOnlyList<AuthorVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAuthorListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<AuthorVm>> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
    {
        var includes =  new List<Expression<Func<Author, object>>>();
        includes.Add(x => x.Books);

        
        var authors = await _unitOfWork.Repository<Author>().GetAsync(
            null,
            x => x.OrderBy(a => a.Name),
            includes,
            true

            );

        var authorsVm = _mapper.Map<IReadOnlyList<AuthorVm>>(authors);
        return authorsVm;
    }
}
