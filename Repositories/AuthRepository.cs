using Microsoft.EntityFrameworkCore;
using MiniEMR.Data;
using MiniEMR.Entities;
using MiniEMR.Enums;
using MiniEMR.Repositories.Interfaces;

namespace MiniEMR.Repositories
{
    public class AuthRepository(AppDbContext _context) : IAuthRepository
    {
        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
        public async Task<User?> GetByUserIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<List<User>> GetDoctorsAsync()
        {
            return await _context.Users
                .Where(u => u.Role == UserRole.Doctor)
                .OrderBy(u => u.FullName)
                .ToListAsync();
        }
    }
}
