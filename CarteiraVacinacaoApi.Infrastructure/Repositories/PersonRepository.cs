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
    public class PersonRepository : IPersonRepository
    {
        private readonly VaccineRecordDbContext _dbContext;

        public PersonRepository(VaccineRecordDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Person> AddSync(Person person)
        {
            _dbContext.Persons.Add(person);
            await _dbContext.SaveChangesAsync();

            return person;
        }

        public async Task<Person> GetById(int personId)
        {
            return await _dbContext.Persons.FirstOrDefaultAsync(p => p.Id == personId);
        }


        public async Task Delete(Person person)
        {
            _dbContext.Persons.Remove(person);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Person> GetVaccinationCard(int personId)
        {
            return await _dbContext.Persons
                .Include(p => p.VaccineRecords)
                .ThenInclude(v => v.Vaccine)
                .FirstOrDefaultAsync(p => p.Id == personId);
        }
    }
}
