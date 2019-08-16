using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Techtalk.FM.Domain.Configurations;
using Techtalk.FM.Domain.Contracts.Repositories;
using Techtalk.FM.Domain.Contracts.Services;
using Techtalk.FM.Domain.DTOs;
using Techtalk.FM.Domain.Extensions;

namespace Techtalk.FM.Domain.Services
{
    public class LoginService : ILoginService
    {
        #region "  Repositories & Configs  "

        private readonly IUserRepository _userRepository;
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly SigningConfigurations _signingConfigurations;

        #endregion

        #region "  Constructors  "

        public LoginService(IUserRepository userRepository, TokenConfigurations tokenConfigurations,
            SigningConfigurations signingConfigurations)
        {
            _userRepository = userRepository;
            _tokenConfigurations = tokenConfigurations;
            _signingConfigurations = signingConfigurations;
        }

        #endregion

        #region "  ILoginService  "

        public async Task<Token> Authenticate(Entities.User user)
        {
            var _user = await _userRepository.LoginSearch(user.Email, user.Password.Cript());

            if (_user != null)
            {
                var identity = new ClaimsIdentity(
                    new GenericIdentity(_user.Id.ToString(), "Login"),
                    new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, _user.Name)
                    }
                );

                DateTime createdAt = DateTime.Now;
                DateTime expiresAt = createdAt.AddSeconds(_tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();

                var securityToken = handler.CreateToken(new SecurityTokenDescriptor()
                {
                    Issuer = null,
                    Audience = null,
                    Subject = identity,
                    NotBefore = createdAt,
                    Expires = expiresAt,
                    SigningCredentials = _signingConfigurations.SigningCredentials
                });

                string token = handler.WriteToken(securityToken);

                return new Token()
                {
                    AccessToken = token,
                    Authenticated = true,
                    CreatedAt = createdAt,
                    ExpiresAt = expiresAt
                };
            }
            else
            {
                throw new ArgumentException("Usuário ou senha inválidos.");
            }
        }

        #endregion
    }
}
