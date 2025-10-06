using CarteiraVacinacaoApi.Domain.Entities;
using CarteiraVacinacaoApi.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Commands.CreateVaccine
{
    public class CreateVaccineHandler : IRequestHandler<CreateVaccineCommand, Vaccine>
    {
        private readonly VaccineRecordDbContext _dbContext;

        public CreateVaccineHandler(VaccineRecordDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Vaccine> Handle(CreateVaccineCommand request, CancellationToken cancellationToken)
        {
            var vaccine = new Vaccine(request.Name, request.DosesRequired);

            _dbContext.Vaccines.Add(vaccine);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return vaccine;
        }
    }
}
