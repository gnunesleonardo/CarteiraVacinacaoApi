using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(string username);
    }
}
