using MiniEMR.Entities;

namespace MiniEMR.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> SearchPatientsAsync(string term);
        Task<Patient?> GetPatientDetailAsync(int patientId);
        Task<List<Patient>> GetPatientsAsync();
        Task<Patient?> GetPatientByIdAsync(int patientId);

        Task<bool> PatientExistsAsync(int patientId);

        Task<bool> CNICExistsAsync(string cnic, int? excludePatientId = null);

        Task CreatePatientAsync(Patient patient);

        void UpdatePatient(Patient patient);

        Task SaveChangesAsync();
    }
}
