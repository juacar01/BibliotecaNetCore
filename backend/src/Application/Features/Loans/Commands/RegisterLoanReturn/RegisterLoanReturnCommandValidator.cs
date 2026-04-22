using Biblioteca.Application.Features.Loans.Commands.RegisterReturn;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Application.Features.Loans.Commands.RegisterLoanReturn;

public class RegisterLoanReturnCommandValidator: AbstractValidator<RegisterLoanReturnCommand>
{
    public RegisterLoanReturnCommandValidator()
    {
    }
}
