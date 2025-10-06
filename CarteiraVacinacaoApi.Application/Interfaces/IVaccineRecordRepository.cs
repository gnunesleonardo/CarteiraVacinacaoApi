using CarteiraVacinacaoApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Interfaces
{
    public interface IVaccineRecordRepository
    {
        Task<VaccineRecord> AddAsync(VaccineRecord vaccineRecord);
        Task<VaccineRecord> GetById(int vaccineRecordId);
        Task<bool> IsDoseNumberValid(int personId, int vaccineId, int doseNumber);
        Task<int> GetLastDoseApplied(int personId, int vaccineId);
        Task Remove(VaccineRecord vaccineRecord);
    }
}
