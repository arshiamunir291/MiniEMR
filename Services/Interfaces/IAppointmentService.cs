using MiniEMR.Enums;
using MiniEMR.Models.AppointmentModels;

namespace MiniEMR.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDashboardResponse> GetAppointmentsAsync(DateTime date, AppointmentStatus? status,int userId);
        Task<AppointmentResponse> CreateAppointmentAsync(CreateAppointment request, int createdByUserId);
        Task CheckInAppointmentAsync(int appointmentId, int updatedByUserId);
        Task CancelAppointmentAsync(int appointmentId, CancelAppointmentRequest request, int updatedByUserId);
        Task<List<string>> GetAvailableSlotsAsync(int doctorId, DateTime date);








    }
}
