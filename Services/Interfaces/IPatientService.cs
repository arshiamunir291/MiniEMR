using System.Threading.Tasks;
using MiniEMR.Models.PatientModels;

namespace MiniEMR.Services.Interfaces
{
    public interface IPatientService
    {
        Task<List<PatientLookup>> SearchPatientsAsync(string term);
        Task<List<PatientList>> GetPatientsAsync();
        Task<PatientDetail?> GetPatientDetailAsync(int patientId);
        Task CreatePatientAsync(CreatePatient patient, int userId);
        Task UpdatePatientAsync(UpdatePatient patient);
    }
}
