using CarteiraVacinacaoApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Interfaces
{
    public interface IVaccineCacheService
    {
        Task<List<Vaccine>> GetAllAsync();
        void ClearCache();
    }
}
