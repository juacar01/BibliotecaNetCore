using System.Security.Cryptography.X509Certificates;

namespace Biblioteca.Domain;


    public class Book : Common.BaseDomainModel
{
    public string Title { get; set; } = null!;
    public int NumberOfPages { get; set; }
    public string Genre { get; set; } = null!;
    public DateTime? PublishedDate { get; set; }
    public String CoverImagePath { get; set; }
    public bool IsDeleted { get; set; } = false;

    public int AuthorId { get; set; }
    // Navigation property for related author
    public virtual Author Author { get; set; } = null!;

    // Navigation property for related loan
    public virtual List<Loan> Loans { get; set; } = new List<Loan>();
}   