using Biblioteca.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Application.Specifications.Loans;

public class LoanForCountingSpecification: BaseSpecification<Loan>
{
    public LoanForCountingSpecification(LoanSpecificationParams loanParams)
        : base(
            x =>
            (!loanParams.BookId.HasValue || x.BookId == loanParams.BookId) &&
            (!loanParams.LoanDate.HasValue || x.LoanDate == loanParams.LoanDate) &&
            (!loanParams.DueDate.HasValue || x.DueDate == loanParams.DueDate)
        )
    {
    }
}
