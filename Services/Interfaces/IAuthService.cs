using MiniEMR.Models.AuthModels;

namespace MiniEMR.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
        Task<UserDetail?> GetUserDetailAsync(int userId);
        Task<List<DoctorLookup>> GetDoctorsAsync();
    }
}
