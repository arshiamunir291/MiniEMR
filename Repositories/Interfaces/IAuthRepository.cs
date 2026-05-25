using MiniEMR.Entities;

namespace MiniEMR.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetByUserNameAsync(string username);
        Task<User?> GetByUserIdAsync(int id);
        Task<List<User>> GetDoctorsAsync();

    }
}
