using Biblioteca.Domain;

namespace Biblioteca.Application.Specifications.Authors;

public class AuthorForCountingSpecification : BaseSpecification<Author>
{
    public AuthorForCountingSpecification(AuthorSpecificationParams authorParams)
    : base(
        x =>
        (string.IsNullOrEmpty(authorParams.Search)
        || string.Concat(x.Name, " ", x.LastName).ToLower().Contains(authorParams.Search.ToLower()))

    )

    {
    }
}
