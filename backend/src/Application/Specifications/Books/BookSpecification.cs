using Biblioteca.Domain;

namespace Biblioteca.Application.Specifications.Books;

public class BookSpecification : BaseSpecification<Book>
{
    public BookSpecification(BookSpecificationParams bookParams)
        : base(x =>
            (string.IsNullOrEmpty(bookParams.Title) || x.Title.ToLower().Contains(bookParams.Title.ToLower())) &&
            (x.AuthorId == bookParams.AuthorId) &&
            !x.IsDeleted)
    {

        ApplyPagging(bookParams.PageSize * (bookParams.PageIndex - 1), bookParams.PageSize);

        if (!string.IsNullOrEmpty(bookParams.Sort))
        {
            switch (bookParams.Sort.ToLower())
            {
                case "title":
                    AddOrderByDescending(x => x.Title);
                    break;
                case "author":
                    AddOrderByDescending(x => x.AuthorId);
                    break;
                default:
                    AddOrderBy(x => x.CreatedAt);
                    break;
            }
        }
        else
        {
            AddOrderByDescending(x => x.CreatedAt);
        }
    }
}
