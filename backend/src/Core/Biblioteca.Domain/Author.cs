namespace Biblioteca.Domain;

public class Author : Common.BaseDomainModel
{
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Biography { get; set; }
    public DateTime? BirthDate { get; set; }
    public String? Country { get; set; }
    public Boolean IsDeleted { get; set; } = false;

    // Navigation property for related books
    public virtual List<Book> Books { get; set; } = new List<Book>();
}