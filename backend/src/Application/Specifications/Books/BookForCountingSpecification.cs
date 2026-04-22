using Biblioteca.Domain;

namespace Biblioteca.Application.Specifications.Books;

public class BookForCountingSpecification : BaseSpecification<Book>
{
    public BookForCountingSpecification(BookSpecificationParams bookParams)
        : base(
            x =>
            (string.IsNullOrEmpty(bookParams.Search) || x.Title.ToLower().Contains(bookParams.Search.ToLower())) &&
            (!bookParams.AuthorId.HasValue || x.AuthorId == bookParams.AuthorId)
        )
    {
    }
}
