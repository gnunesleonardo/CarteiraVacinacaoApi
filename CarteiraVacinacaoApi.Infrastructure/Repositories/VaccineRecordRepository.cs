using CarteiraVacinacaoApi.Application.Interfaces;
using CarteiraVacinacaoApi.Domain.Entities;
using CarteiraVacinacaoApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Infrastructure.Repositories
{
    public class VaccineRecordRepository : IVaccineRecordRepository
    {
        private readonly VaccineRecordDbContext _dbContext;

        public VaccineRecordRepository(VaccineRecordDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VaccineRecord> AddAsync(VaccineRecord vaccineRecord)
        {
            _dbContext.VaccineRecords.Add(vaccineRecord);
            await _dbContext.SaveChangesAsync();

            return vaccineRecord;
        }

        public async Task<VaccineRecord> GetById(int vaccineRecordId)
        {
            return await _dbContext.VaccineRecords
                .FirstOrDefaultAsync(vr => vr.Id == vaccineRecordId);
        }

        public async Task<bool> DoseNumberAlreadyExists(int personId, int vaccineId, int doseNumber)
        {
            var record = await _dbContext.VaccineRecords
                .FirstOrDefaultAsync(vr => vr.PersonId == personId && vr.VaccineId == vaccineId && vr.DoseNumber == doseNumber);

            return record != null;
        }

        public async Task<int> GetLastDoseApplied(int personId, int vaccineId)
        {
            return await _dbContext.VaccineRecords
                .Where(vr => vr.PersonId == personId && vr.VaccineId == vaccineId)
                .MaxAsync(vr => vr.DoseNumber);
        }

        public async Task Remove(VaccineRecord vaccineRecord)
        {
            _dbContext.VaccineRecords.Remove(vaccineRecord);
            await _dbContext.SaveChangesAsync();
        }
    }
}
