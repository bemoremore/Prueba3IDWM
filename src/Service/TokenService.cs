using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Prueba3_Backend.src.Interfaces;
using Prueba3_Backend.src.Models;

namespace Prueba3_Backend.src.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        private readonly UserManager<User> _userManager;

        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            var singingKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(singingKey))
            {
                throw new ArgumentNullException(nameof(singingKey), "JWT signing key cannot be null or empty.");
            }
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(singingKey));
        }

        public async Task<string> CreateTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
                
            var tokenDescriptor = new SecurityTokenDescriptor
            {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return await Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}