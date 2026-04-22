using Biblioteca.Application.Features.Books.Queries.Vms;
using Biblioteca.Application.Models.Loan;
using Biblioteca.Domain;


namespace Biblioteca.Application.Features.Loans.Queries.Vms;

public class LoanVm
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public String BorrowerName { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public virtual BookVm Book { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public LoanStatus Status
    {
        get
        {
            if (ReturnDate == null)
            {
                return LoanStatus.Activo;
            }

            if (DateTime.Now > DueDate && ReturnDate == null)
            {
                return LoanStatus.Retrasado;
            }

            return LoanStatus.Inactivo;
        }
    }

    public string StatusLabel
    {
        get
        {
            switch (Status)
            {
                case LoanStatus.Activo:
                    {
                        return LoanStatusLabel.ACTIVO;
                    }

                case LoanStatus.Inactivo:
                    {
                        return LoanStatusLabel.INACTIVO;
                    }
                case LoanStatus.Retrasado:
                    {
                        return LoanStatusLabel.RETRASADO;
                    }

                default: return LoanStatusLabel.INACTIVO;
            }
        }
        set { }
    }

}
