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
    public class GetAllVaccinesHandler : IRequestHandler<GetAllVaccineQuery, List<Vaccine>>
    {
        private readonly IVaccineCacheService _vaccineCache;

        public GetAllVaccinesHandler(IVaccineCacheService vaccineCache)
        {
            _vaccineCache = vaccineCache;
        }

        public async Task<List<Vaccine>> Handle(GetAllVaccineQuery request, CancellationToken cancellationToken)
        {
            return await _vaccineCache.GetAllAsync();
        }
    }
}
