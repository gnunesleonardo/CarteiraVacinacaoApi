using CarteiraVacinacaoApi.Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Commands.DeleteVaccineRecord
{
    public class DeleteVaccineRecordValidator : AbstractValidator<DeleteVaccineRecordCommand>
    {
        public DeleteVaccineRecordValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id da pessoa não pode ser nulo.")
                .GreaterThan(0).WithMessage("Id da pessoa deve ser maior que zero.");
        }
    }
}
