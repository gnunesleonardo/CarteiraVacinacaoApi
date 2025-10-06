using CarteiraVacinacaoApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Interfaces
{
    public interface IVaccineRepository
    {
        Task<Vaccine> AddAsync(Vaccine vaccine);
        Task<List<Vaccine>> GetAll();
    }
}
