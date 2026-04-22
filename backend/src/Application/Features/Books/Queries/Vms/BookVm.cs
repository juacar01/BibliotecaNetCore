using Biblioteca.Domain;

namespace Biblioteca.Application.Features.Books.Queries.Vms;

public class BookVm
{
    public int Id { get; set; }

    public string Title { get; set; }
    public int NumberOfPages { get; set; }
    public string Genre { get; set; }
    public DateTime? PublishedDate { get; set; }
    public String CoverImagePath { get; set; }




    // Navigation property for related author
    public virtual Author Author { get; set; }

    // Navigation property for related loan
    //public virtual List<Loan> Loans { get; set; } = new List<Loan>();

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}
