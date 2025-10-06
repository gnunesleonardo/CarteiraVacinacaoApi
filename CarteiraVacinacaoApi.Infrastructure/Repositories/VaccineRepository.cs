using CarteiraVacinacaoApi.Application.Interfaces;
using CarteiraVacinacaoApi.Domain.Entities;
using CarteiraVacinacaoApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Infrastructure.Repositories
{
    public class VaccineRepository : IVaccineRepository
    {
        private readonly VaccineRecordDbContext _dbContext;

        public VaccineRepository(VaccineRecordDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Vaccine> AddAsync(Vaccine vaccine)
        {
            _dbContext.Vaccines.Add(vaccine);
            await _dbContext.SaveChangesAsync();
            return vaccine;
        }

        public async Task<List<Vaccine>> GetAll()
        {
            return await _dbContext.Vaccines.AsNoTracking().ToListAsync();
        }
    }
}
