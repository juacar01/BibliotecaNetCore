namespace Biblioteca.Application.Features.Authors.Queries.Vms;

public class AuthorVm
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string? Biography { get; set; }
    public DateTime? BirthDate { get; set; }
    public String? Country { get; set; }
    public Boolean IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation property for related books
    //public virtual List<BookVm> Books { get; set; } = new List<BookVm>();

}
