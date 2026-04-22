namespace Biblioteca.Application.Specifications.Books;

public class BookSpecificationParams : SpecificationParams
{
        public string? Title { get; set; }
        public int? AuthorId { get; set; } = 0;
        public bool? IsDeleted { get; set; }

}
