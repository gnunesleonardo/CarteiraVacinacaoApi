using CarteiraVacinacaoApi.Application.DTOs;
using CarteiraVacinacaoApi.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Queries.GetVaccinationCard
{
    public class GetVaccinationCardHandler : IRequestHandler<GetVaccinationCardQuery, VaccinationCardDto>
    {
        private readonly IPersonRepository _personRepository;

        public GetVaccinationCardHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<VaccinationCardDto> Handle(GetVaccinationCardQuery request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetVaccinationCard(request.PersonId);

            if (person == null)
                throw new KeyNotFoundException("Pessoa não encontrada.");

            return new VaccinationCardDto
            {
                PersonId = person.Id,
                PersonName = person.Name,
                VaccineRecords = person.VaccineRecords
                    .Select(vr => new VaccineRecordDto
                    {
                        RecordId = vr.Id,
                        DoseNumber = vr.DoseNumber,
                        AppliedDate = vr.AppliedDate,
                        Vaccine = new VaccineDto
                        {
                            VaccineId = vr.Vaccine.Id,
                            VaccineName = vr.Vaccine.Name,
                            DosesRequired = vr.Vaccine.DosesRequired
                        }
                    })
                .OrderBy(v => v.AppliedDate)
                .ToList()
            };
        }
    }
}
