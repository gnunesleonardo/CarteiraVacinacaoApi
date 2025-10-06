using CarteiraVacinacaoApi.Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Commands.CreateVaccineRecord
{
    public class CreateVaccineRecordValidator : AbstractValidator<CreateVaccineRecordCommand>
    {
        private readonly IVaccineCacheService _vaccineCacheService;

        public CreateVaccineRecordValidator(IVaccineCacheService vaccineCacheService)
        {
            _vaccineCacheService = vaccineCacheService;

            RuleFor(x => x.PersonId)
                .NotNull().WithMessage("Id da pessoa não pode ser nulo.")
                .GreaterThan(0).WithMessage("Id da pessoa deve ser maior que zero.");

            RuleFor(x => x.VaccineId)
                .NotNull().WithMessage("Id da vacina não pode ser nulo")
                .GreaterThan(0).WithMessage("Id da vacina deve ser maior que zero.");

            RuleFor(x => x.AppliedDate)
                .NotNull().WithMessage("Data de aplicação inválida.");

            RuleFor(v => v)
                .MustAsync(async (command, cancellation) =>
                {
                    var vaccines = await _vaccineCacheService.GetAllAsync();
                    var vaccine = vaccines.FirstOrDefault(v => v.Id == command.VaccineId);

                    if (command.DoseNumber > vaccine.DosesRequired)
                        return false;

                    return true;
                })
                .WithMessage("Número da dose inválido.");
        }
    }
}
