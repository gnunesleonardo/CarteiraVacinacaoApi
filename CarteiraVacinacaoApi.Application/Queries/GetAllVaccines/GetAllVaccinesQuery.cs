using CarteiraVacinacaoApi.Application.DTOs;
using CarteiraVacinacaoApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Queries.GetAllVaccines
{
    public record GetAllVaccineQuery() : IRequest<List<VaccineDto>>;
}
