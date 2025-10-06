using CarteiraVacinacaoApi.Domain.Entities;
using CarteiraVacinacaoApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Queries.GetAllVaccines
{
    public class GetAllVaccinesHandler : IRequestHandler<GetAllVaccineQuery, List<Vaccine>>
    {
        private readonly VaccineRecordDbContext _dbContext;

        public GetAllVaccinesHandler(VaccineRecordDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Vaccine>> Handle(GetAllVaccineQuery request, CancellationToken cancellationToken)
        {
            var vaccines = await _dbContext.Vaccines.AsNoTracking().ToListAsync(cancellationToken);

            return vaccines;
        }
    }
}
