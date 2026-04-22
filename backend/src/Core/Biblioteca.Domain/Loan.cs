namespace Biblioteca.Domain;

public class Loan : Common.BaseDomainModel
{
    public int BookId { get; set; }
    public String BorrowerName { get; set; }
    public DateTime LoanDate { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; } = null!;
    // Navigation properties
    public virtual Book Book { get; set; } = null!;

}