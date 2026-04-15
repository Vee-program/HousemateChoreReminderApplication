using Chores.Core.Interfaces;
using HousemateChoreReminderAPI.Chores.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;

namespace Chores.Core.Services
{
    public  class AuthService: IAuthService
    {
        private readonly IHousemateRepository _housemateRepository;

        private readonly IConfiguration _configuration;

        public AuthService(IHousemateRepository housemateRepository, IConfiguration configuration)
        {
            _housemateRepository = housemateRepository;
            _configuration = configuration;
        }
        public async Task<string> Register(Housemate housemate, string password)
        {
            if (await _housemateRepository.GetAdmin() != null)
                throw new InvalidOperationException("Admin already exists");

            housemate.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            housemate.IsAdmin = true;

            await _housemateRepository.AddHousemate(housemate);

            return "Registration successful";
        }

        public async Task<string> Login(string username, string? password)
        {
           var housemate = await _housemateRepository.GetHousemateByUsername(username);

            if (housemate == null)
                throw new KeyNotFoundException("Housemate not found");

            if (password != null)
            {
                // Admin login - verify password
                if (!BCrypt.Net.BCrypt.Verify(password, housemate.PasswordHash))
                    throw new UnauthorizedAccessException("Invalid credentials");
            }
            // If password is null - housemate login, username match is enough

            //data you put inside a token 
            var claims = new[]
               {
             new Claim("id", housemate.Id.ToString()),
             new Claim("username", housemate.Username),
             new Claim("isAdmin", housemate.IsAdmin.ToString())

            
              };

            //secret key that proves the token came from your server 
            var key = new SymmetricSecurityKey(
             Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
             issuer: _configuration["Jwt:Issuer"],
              audience: _configuration["Jwt:Audience"],
              claims: claims,
             expires: DateTime.UtcNow.AddHours(8),
             signingCredentials: credentials
                               );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> ForgotUsername(string phoneNumber)
        {
            var housemate = await _housemateRepository.GetHousemateByPhoneNumber(phoneNumber);

            if (housemate == null)
                throw new InvalidOperationException("Phone number not found");

            return housemate.Username;

        }
    }
}