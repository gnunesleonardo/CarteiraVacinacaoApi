using CarteiraVacinacaoApi.Application.Commands.CreateVaccine;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Commands.CreatePerson
{
    internal class CreatePersonValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonValidator()
        {
            RuleFor(x => x.PersonName)
                .NotEmpty().WithMessage("Nome da pessoa é obrigatório.")
                .MaximumLength(128).WithMessage("Nome não pode ultrapassar 128 caracteres.");
        }
    }
}
