using CarteiraVacinacaoApi.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IAdminCredentialsValidator _adminCredentialsValidator;

        public LoginHandler(IJwtProvider jwtProvider, IAdminCredentialsValidator adminCredentialsValidator)
        {
            _jwtProvider = jwtProvider;
            _adminCredentialsValidator = adminCredentialsValidator;
        }

        public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (!_adminCredentialsValidator.Validate(request.Username, request.Password))
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            var token = _jwtProvider.GenerateToken(request.Username);

            return Task.FromResult(token);
        }
    }
}
