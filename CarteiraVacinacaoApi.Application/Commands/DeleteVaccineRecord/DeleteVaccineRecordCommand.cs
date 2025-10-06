using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Commands.DeleteVaccineRecord
{
    public record DeleteVaccineRecordCommand(int Id) : IRequest;
}
