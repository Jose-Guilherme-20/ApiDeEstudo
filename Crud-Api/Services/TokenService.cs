using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Crud_Api.Repository.Interfaces;
using Crud_Api.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using Services.Interfaces;

namespace Crud_Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly string Key;
        private readonly IPersonRepository _personRepository;

        public TokenService(IConfiguration configuration, IPersonRepository personRepository)
        {
            _configuration = configuration;
            Key = _configuration["Key"];
            _personRepository = personRepository;
        }

        public string GenerateToken(Person person)
        {
            var user = _personRepository.LoginPerson(person);
            if (user == null)
            {
                return null;
            }
            var tokenHadler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.LastName)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHadler.CreateToken(tokenDescriptor);
            return tokenHadler.WriteToken(token);
        }
    }
}