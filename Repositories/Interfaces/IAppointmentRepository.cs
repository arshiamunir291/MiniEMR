using MiniEMR.Entities;
using MiniEMR.Enums;

namespace MiniEMR.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime date, AppointmentStatus? status);
        Task<List<Appointment>> GetDoctorTodayAppointmentsAsync(int doctorId, DateTime today);
        Task<List<Appointment>> GetAppointmentsForSummaryAsync(DateTime date);
        Task AddAsync(Appointment appointment);
        Task<Appointment?> GetByIdAsync(int appointmentId);
        Task UpdateAsync(Appointment appointment);

        Task SaveChangesAsync();
        Task<List<Appointment>> GetExpiredAppointmentsAsync();
        Task<List<string>> GetAvailableSlotsAsync(int doctorId, DateTime date);




    }
}
