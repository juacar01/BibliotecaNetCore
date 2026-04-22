using AutoMapper;
using Biblioteca.Application.Features.Books.Commands.CreateBook;
using Biblioteca.Application.Features.Books.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.Features.Books.Commands;

public class CreateBookCommandHandler: IRequestHandler<CreateBookCommand, BookVm>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<BookVm> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var bookEntity = _mapper.Map<Book>(request);
        await _unitOfWork.Repository<Book>().AddAsync(bookEntity);

        return _mapper.Map<BookVm>(bookEntity);
    }
}
