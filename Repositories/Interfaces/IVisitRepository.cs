using MiniEMR.Entities;

namespace MiniEMR.Repositories.Interfaces
{
    public interface IVisitRepository
    {
        Task<Appointment?> GetAppointmentForVisitAsync(int appointmentId);
        Task AddVisitAsync(Visit visit);
        Task<List<Drug>> GetDrugsAsync();
        Task<bool> VisitExistsForAppointmentAsync(int appointmentId);
        Task SaveChangesAsync();
    }
}
