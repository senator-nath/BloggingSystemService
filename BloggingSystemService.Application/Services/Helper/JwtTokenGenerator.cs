using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Services.Helper
{
    public class JwtTokenGenerator
    {
        private readonly AppSettings _appSettings;

        public JwtTokenGenerator(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateToken(string AuthorName)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, AuthorName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var expiration = DateTime.UtcNow.AddDays(1);


            var tokenDescriptor = new JwtSecurityToken(
                issuer: _appSettings.Issuer,
                audience: _appSettings.Audience,
                expires: expiration,
                signingCredentials: credentials,
                claims: claims
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenDescriptor);


        }
    }
}

