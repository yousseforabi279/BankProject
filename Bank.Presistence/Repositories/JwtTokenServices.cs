using bank.Domain.Entities;
using Bank.Application.contracts;
using Bank.Presistence.Dbcontext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Presistence.Repositories
{
    public class JwtTokenServices : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnityOfWork _unitOfWork;
        private readonly Appcontext _appcontext;


        public JwtTokenServices(IConfiguration configuration, IUnityOfWork unitOfWork,Appcontext appcontext)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _appcontext = appcontext;
        }

        public string GenerateAccessToken(string userId, string email, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new(JwtRegisteredClaimNames.Email, email),
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Email, email)
                };
        
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken(string userId)
        {
            var token = Convert.ToBase64String(
               RandomNumberGenerator.GetBytes(64));
            var refreshToken = new RefreshToken
            {
                Token = token,
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt= DateTime.UtcNow

            };

            _appcontext.RefreshTokens.Add(refreshToken);
            return token;
        }

    }
}
