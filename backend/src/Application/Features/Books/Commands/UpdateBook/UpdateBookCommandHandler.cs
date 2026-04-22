using AutoMapper;
using Biblioteca.Application.Features.Books.Queries.Vms;
using Biblioteca.Application.Persistence;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.Features.Books.Commands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookVm> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var bookEntity = await _unitOfWork.Repository<Book>().GetByIdAsync(request.BookId); ;

        bookEntity.AuthorId = request.AuthorId;
        bookEntity.Title = request.Title;
        bookEntity.NumberOfPages = request.NumberOfPages;
        bookEntity.PublishedDate = request.PublishedDate;
        bookEntity.CoverImagePath = request.CoverImagePath;
        bookEntity.Genre = request.Genre;

        await _unitOfWork.Repository<Book>().UpdateAsync(bookEntity);

        return _mapper.Map<BookVm>(bookEntity);
    }
}
