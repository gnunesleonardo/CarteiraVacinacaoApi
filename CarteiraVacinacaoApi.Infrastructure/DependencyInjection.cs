using CarteiraVacinacaoApi.Application.Interfaces;
using CarteiraVacinacaoApi.Infrastructure.Caching;
using CarteiraVacinacaoApi.Infrastructure.Persistence;
using CarteiraVacinacaoApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region DATABASE
            services.AddDbContext<VaccineRecordDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region CACHING
            services.AddScoped<IVaccineCacheService, VaccineCacheService>();
            #endregion

            #region REPOSITORIES
            services.AddScoped<IVaccineRepository, VaccineRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IVaccineRecordRepository, VaccineRecordRepository>();
            #endregion

            return services;
        }
    }
}
