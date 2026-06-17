using MiniEMR.Models.VisitModels;

namespace MiniEMR.Services.Interfaces
{
    public interface IVisitService
    {
        Task<StartVisit> StartVisitAsync(int appointmentId, int currentDoctorId);
        Task SaveVisitAsync(SaveVisit model, int currentDoctorId);
        Task<StartVisit> StartEmergencyVisitAsync(int patientId, int currentDoctorId);
        Task<List<DrugDropDown>> GetDrugsAsync();
    }
}
