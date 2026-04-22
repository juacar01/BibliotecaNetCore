namespace Biblioteca.Application.Specifications.Authors;

public class AuthorSpecificationParams : SpecificationParams
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public bool? IsDeleted { get; set; }
}
