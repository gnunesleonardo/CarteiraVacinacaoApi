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
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome da vacina é obrigatório.")
                .MaximumLength(128).WithMessage("Nome não pode passar de 128 caracteres.");
        }
    }
}
