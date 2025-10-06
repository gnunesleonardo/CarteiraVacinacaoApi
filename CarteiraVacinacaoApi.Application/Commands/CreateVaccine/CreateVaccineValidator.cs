using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Commands.CreateVaccine
{
    public class CreateVaccineValidator : AbstractValidator<CreateVaccineCommand>
    {
        public CreateVaccineValidator()
        {
            RuleFor(x => x.VaccineName)
                .NotEmpty().WithMessage("Nome da vacina é obrigatório.")
                .MaximumLength(128).WithMessage("Nome não pode ultrapassar 128 caracteres.");

            RuleFor(x => x.DosesRequired)
                .NotEmpty().WithMessage("Quantidade de doses é obrigatório.")
                .GreaterThan(0).WithMessage("Quantidade de doses deve ser maior que zero.");
        }
    }
}
