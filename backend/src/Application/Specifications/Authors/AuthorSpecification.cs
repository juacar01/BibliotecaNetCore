using Biblioteca.Application.Specifications.Books;
using Biblioteca.Domain;

namespace Biblioteca.Application.Specifications.Authors;

public class AuthorSpecification: BaseSpecification<Author>
{
    public AuthorSpecification(AuthorSpecificationParams authorParams)
    : base(
        x =>
        (string.IsNullOrEmpty(authorParams.Search) 
        || string.Concat(x.Name, " ", x.LastName).ToLower().Contains(authorParams.Search.ToLower()))

    )
    {

        ApplyPagging(authorParams.PageSize * (authorParams.PageIndex - 1), authorParams.PageSize);

        if (!string.IsNullOrEmpty(authorParams.Sort))
        {
            switch (authorParams.Sort.ToLower())
            {
                case "name":
                    AddOrderByDescending(x => x.Name);
                    break;
                case "lastname":
                    AddOrderByDescending(x => x.LastName);
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
