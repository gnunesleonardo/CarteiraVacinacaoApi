using CarteiraVacinacaoApi.Application.DTOs;
using CarteiraVacinacaoApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Commands.CreateVaccineRecord
{
    public record CreateVaccineRecordCommand(int PersonId, int VaccineId, int DoseNumber, DateTime AppliedDate) : IRequest<VaccineRecordDto>;
}
