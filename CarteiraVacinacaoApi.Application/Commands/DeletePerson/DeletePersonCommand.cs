using CarteiraVacinacaoApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Commands.DeletePerson
{
    public class DeletePersonCommand(int id) : IRequest
    {
        public int Id { get; set; } = id;
    }
}
