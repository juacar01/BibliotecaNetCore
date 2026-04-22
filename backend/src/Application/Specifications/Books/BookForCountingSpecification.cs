using Biblioteca.Domain;

namespace Biblioteca.Application.Specifications.Books;

public class BookForCountingSpecification : BaseSpecification<Book>
{
    public BookForCountingSpecification(BookSpecificationParams bookParams)
        : base(x =>
            (string.IsNullOrEmpty(bookParams.Title) || x.Title.ToLower().Contains(bookParams.Title.ToLower())) &&
            (x.AuthorId == bookParams.AuthorId) &&
            !x.IsDeleted)
    {
    }
}
