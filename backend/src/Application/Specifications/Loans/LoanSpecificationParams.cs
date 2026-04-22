using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Application.Specifications.Loans;

public class LoanSpecificationParams : SpecificationParams
{
    public int? BookId { get; set; }
    public DateTime? LoanDate { get; set; }
    public DateTime? DueDate { get; set; }
}
