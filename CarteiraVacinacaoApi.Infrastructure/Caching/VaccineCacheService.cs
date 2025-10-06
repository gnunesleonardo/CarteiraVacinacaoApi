using CarteiraVacinacaoApi.Application.Interfaces;
using CarteiraVacinacaoApi.Domain.Entities;
using CarteiraVacinacaoApi.Infrastructure.Persistence;
using CarteiraVacinacaoApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Infrastructure.Caching
{
    public class VaccineCacheService : IVaccineCacheService
    {
        private readonly IMemoryCache _cache;
        private readonly IVaccineRepository _vaccineRepository;

        private const string CACHE_KEY = "VACCINE_CACHE";
        private const int EXPIRATION_MIN = 60;

        public VaccineCacheService(IVaccineRepository vaccineRepository, IMemoryCache cache)
        {
            _vaccineRepository = vaccineRepository;
            _cache = cache;
        }

        public async Task<List<Vaccine>> GetAllAsync()
        {
            if (!_cache.TryGetValue(CACHE_KEY, out List<Vaccine>? vaccines))
            {
                vaccines = await _vaccineRepository.GetAll();
                _cache.Set(CACHE_KEY, vaccines, TimeSpan.FromMinutes(EXPIRATION_MIN));
            }

            return vaccines!;
        }

        public void ClearCache() => _cache.Remove(CACHE_KEY);
    }
}
