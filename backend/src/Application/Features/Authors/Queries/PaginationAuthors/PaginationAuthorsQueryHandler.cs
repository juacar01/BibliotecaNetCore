using AutoMapper;
using Biblioteca.Application.Features.Authors.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Application.Shared.Queries;
using Biblioteca.Application.Specifications.Authors;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.Features.Authors.Queries.PaginationAuthors;

public class PaginationAuthorsQueryHandler : IRequestHandler<PaginationAuthorsQuery, PaginationVm<AuthorVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaginationAuthorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationVm<AuthorVm>> Handle(PaginationAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authorSpecParams = new AuthorSpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
            Name = request.Name,
            LastName = request.LastName,
            IsDeleted = request.IsDeleted

        };

        var spec = new AuthorSpecification(authorSpecParams);
        var authors = await _unitOfWork.Repository<Author>().GetAllWithSpec(spec);


        var specCount = new AuthorForCountingSpecification(authorSpecParams);
        var totalItems = await _unitOfWork.Repository<Author>().CountAsync(specCount);

        var rounded = Math.Ceiling((decimal)totalItems / request.PageSize);
        var totalPages = (int)rounded;

        var data = _mapper.Map<IReadOnlyList<Author>, IReadOnlyList<AuthorVm>>(authors);
        var booksByPage = authors.Count();

        var paginationVm = new PaginationVm<AuthorVm>
        {
            Count = totalItems,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Data = data,
            PageCount = totalPages,
            ResultByPage = booksByPage
        };

        return paginationVm;
    }
}
