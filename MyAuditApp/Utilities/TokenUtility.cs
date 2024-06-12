using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class TokenUtility
    {
        private readonly IConfiguration _configuration;
        public TokenUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GenerateToken(string email)
        {
            var key = _configuration["AuditSecretKey"];
            var keyBytes=Encoding.UTF8.GetBytes(key);
            var KeyOne=new SymmetricSecurityKey(keyBytes);
            var signingCredentials = new SigningCredentials(KeyOne,SecurityAlgorithms.HmacSha256);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires=DateTime.UtcNow.AddHours(1),
                SigningCredentials=signingCredentials
            };
            var token=tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(token);
        }
    }
}
