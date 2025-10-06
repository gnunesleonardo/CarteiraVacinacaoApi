using CarteiraVacinacaoApi.Application.DTOs;
using CarteiraVacinacaoApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> AddSync(Person person);
        Task<Person> GetById(int personId);
        Task Delete(Person person);
        Task<Person> GetVaccinationCard(int personId);
    }
}
