using AutoMapper;
using Biblioteca.Application.Features.Books.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.Features.Books.Commands.DeleteBook;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, BookVm>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookVm> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Repository<Book>().GetByIdAsync(request.BookId);
        book.IsDeleted = true;
        await _unitOfWork.Repository<Book>().UpdateAsync(book);

        return _mapper.Map<BookVm>(book);
    }
}
