using Biblioteca.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Application.Specifications.Loans;

public class LoanSpecification: BaseSpecification<Loan>
{
    public LoanSpecification(LoanSpecificationParams loanParams)
        : base(
            x =>
            (!loanParams.BookId.HasValue || x.BookId == loanParams.BookId) &&
            (!loanParams.LoanDate.HasValue || x.LoanDate == loanParams.LoanDate) &&
            (!loanParams.DueDate.HasValue || x.DueDate == loanParams.DueDate)
        )
    {
        AddInclude(x => x.Book);
        AddOrderByDescending(x => x.LoanDate);


        ApplyPagging(loanParams.PageSize * (loanParams.PageIndex - 1), loanParams.PageSize);

        if (!string.IsNullOrEmpty(loanParams.Sort))
        {
            switch (loanParams.Sort.ToLower())
            {
                case "book":
                    AddOrderByDescending(x => x.BookId);
                    break;
                default:
                    AddOrderBy(x => x.CreatedAt);
                    break;
            }
        }
        else
        {
            AddOrderByDescending(x => x.CreatedAt);
        }

    }
}
