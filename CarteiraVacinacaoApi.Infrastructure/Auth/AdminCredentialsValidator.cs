using CarteiraVacinacaoApi.Application.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Infrastructure.Auth
{
    public class AdminCredentialsValidator : IAdminCredentialsValidator
    {
        private readonly AdminCredentials _adminCredentials;
        
        public AdminCredentialsValidator(IOptions<AdminCredentials> adminCredentials)
        {
            _adminCredentials = adminCredentials.Value;
        }

        public bool Validate(string username, string password)
        {
            return _adminCredentials.Username == username && _adminCredentials.Password == password;
        }
    }
}
