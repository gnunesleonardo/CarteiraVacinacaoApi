using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Interfaces
{
    public interface IAdminCredentialsValidator
    {
        bool Validate(string username, string password);
    }
}
