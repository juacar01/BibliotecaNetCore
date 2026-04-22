using Biblioteca.Application.Features.Authors.Queries.Vms;
using Biblioteca.Application.Features.Loans.Queries.Vms;

namespace Biblioteca.Application.Features.Books.Queries.Vms;

public class BookVm
{
    public int Id { get; set; }

    public string Title { get; set; }
    public int NumberOfPages { get; set; }
    public string Genre { get; set; }
    public DateTime? PublishedDate { get; set; }
    public String CoverImagePath { get; set; }
    public bool IsDeleted { get; set; }




    // Navigation property for related author
    public virtual AuthorVm Author { get; set; }

    //public virtual ICollection<LoanVm>? Loans { get; set; }



    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}
