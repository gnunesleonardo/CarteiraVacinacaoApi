using CarteiraVacinacaoApi.Application.DTOs;
using CarteiraVacinacaoApi.Application.Interfaces;
using CarteiraVacinacaoApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Commands.CreateVaccineRecord
{
    public class CreateVaccineRecordHandler : IRequestHandler<CreateVaccineRecordCommand, VaccineRecordDto>
    {
        private readonly IVaccineRecordRepository _vaccineRecordRepository;
        private readonly IVaccineCacheService _vaccineCacheService;
        private readonly IPersonRepository _personRepository;

        public CreateVaccineRecordHandler(
            IVaccineRecordRepository vaccineRecordRepository, 
            IVaccineCacheService vaccineCacheService, 
            IPersonRepository personRepository)
        {
            _vaccineRecordRepository = vaccineRecordRepository;
            _personRepository = personRepository;
            _vaccineCacheService = vaccineCacheService;
        }

        public async Task<VaccineRecordDto> Handle(CreateVaccineRecordCommand request, CancellationToken cancellationToken)
        {
            var requestData = new VaccineRecord(
                request.PersonId, 
                request.VaccineId, 
                request.DoseNumber, 
                request.AppliedDate);

            var vaccines = await _vaccineCacheService.GetAllAsync();
            var vaccine = vaccines.FirstOrDefault(v => v.Id == request.VaccineId);
            if (vaccine == null)
                throw new KeyNotFoundException("Vacina não encontrada.");

            var person = await _personRepository.GetById(request.PersonId);
            if (person == null)
                throw new KeyNotFoundException("Pessoa não encontrada.");

            var doseNumberAlreadyExists = await _vaccineRecordRepository.DoseNumberAlreadyExists(
                request.PersonId, 
                request.VaccineId,
                request.DoseNumber);
            
            if (doseNumberAlreadyExists)
                throw new ArgumentException("Número de dose já cadastrado.");

            var lastDoseNumber = await _vaccineRecordRepository.GetLastDoseApplied(person.Id, vaccine.Id);
            if (request.DoseNumber != (lastDoseNumber + 1))
                throw new ArgumentException("Número de dose inválido.");

            var vaccineRecord = await _vaccineRecordRepository.AddAsync(requestData);

            return new VaccineRecordDto
            {
                RecordId = vaccineRecord.Id,
                DoseNumber = vaccineRecord.DoseNumber,
                AppliedDate = vaccineRecord.AppliedDate,
                Vaccine = new VaccineDto
                {
                    VaccineId = vaccine.Id,
                    VaccineName = vaccine.Name,
                    DosesRequired = vaccine.DosesRequired
                }
            };
        }
    }
}
