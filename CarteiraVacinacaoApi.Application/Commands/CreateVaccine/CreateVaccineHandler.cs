using CarteiraVacinacaoApi.Application.DTOs;
using CarteiraVacinacaoApi.Application.Interfaces;
using CarteiraVacinacaoApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Commands.CreateVaccine
{
    public class CreateVaccineHandler : IRequestHandler<CreateVaccineCommand, VaccineDto>
    {
        private readonly IVaccineRepository _vaccineRepository;

        public CreateVaccineHandler(IVaccineRepository vaccineRepository)
        {
            _vaccineRepository = vaccineRepository;
        }

        public async Task<VaccineDto> Handle(CreateVaccineCommand request, CancellationToken cancellationToken)
        {
            var vaccine = await _vaccineRepository.AddAsync(new Vaccine(request.VaccineName, request.DosesRequired));

            return new VaccineDto
            {
                VaccineId = vaccine.Id,
                VaccineName = vaccine.Name,
                DosesRequired = vaccine.DosesRequired
            };
        }
    }
}
