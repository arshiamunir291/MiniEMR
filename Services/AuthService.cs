using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MiniEMR.Entities;
using MiniEMR.Models.AuthModels;
using MiniEMR.Repositories.Interfaces;
using MiniEMR.Services.Interfaces;
namespace MiniEMR.Services
{
    public class AuthService(IAuthRepository authRepository, IConfiguration config) : IAuthService
    {
        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await authRepository.GetByUserNameAsync(request.UserName);
            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) return null;
            var token = GenerateJwt(user);
            return new LoginResponse
            {
                Token = token,
                UserId = user.UserId,
                FullName = user.FullName,
                Role = user.Role.ToString()
            };
        }
        public async Task<UserDetail?> GetUserDetailAsync(int userId)
        {
            var user = await authRepository.GetByUserIdAsync(userId);
            if (user == null) return null;
            return new UserDetail
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Role = user.Role.ToString(),
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Specialization = user.Specialization
            };
        }
        private string GenerateJwt(User user)
        {
            var claims = new[]
            {
            new Claim("userId", user.UserId.ToString()),
            new Claim("username", user.UserName),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["Jwt:Key"]!));

            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
