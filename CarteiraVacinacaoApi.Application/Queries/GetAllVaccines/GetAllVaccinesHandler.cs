using CarteiraVacinacaoApi.Application.DTOs;
using CarteiraVacinacaoApi.Application.Interfaces;
using CarteiraVacinacaoApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Queries.GetAllVaccines
{
    public class GetAllVaccinesHandler : IRequestHandler<GetAllVaccineQuery, List<VaccineDto>>
    {
        private readonly IVaccineCacheService _vaccineCache;

        public GetAllVaccinesHandler(IVaccineCacheService vaccineCache)
        {
            _vaccineCache = vaccineCache;
        }

        public async Task<List<VaccineDto>> Handle(GetAllVaccineQuery request, CancellationToken cancellationToken)
        {
            List<VaccineDto> vaccineDtos = new();
            var vaccines = await _vaccineCache.GetAllAsync();

            vaccineDtos = vaccines.Select(v => new VaccineDto { 
                VaccineId = v.Id, 
                VaccineName = v.Name, 
                DosesRequired = v.DosesRequired 
            }).ToList();

            return vaccineDtos;
        }
    }
}
