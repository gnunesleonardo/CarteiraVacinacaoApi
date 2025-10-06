using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Commands.DeletePerson
{
    internal class DeletePersonValidator : AbstractValidator<DeletePersonCommand>
    {
        public DeletePersonValidator() => RuleFor(x => x.Id).NotNull().GreaterThan(0).WithMessage("Id inválido.");
    }
}
