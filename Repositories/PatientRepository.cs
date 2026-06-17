using Microsoft.EntityFrameworkCore;
using MiniEMR.Data;
using MiniEMR.Entities;
using MiniEMR.Repositories.Interfaces;

namespace MiniEMR.Repositories
{
    public class PatientRepository(AppDbContext _context) : IPatientRepository
    {
        public async Task<List<Patient>> SearchPatientsAsync(string term)
        {
            return await _context.Patients
                .AsNoTracking()
                .Where(p => string.IsNullOrWhiteSpace(term) || p.FirstName.Contains(term) || p.LastName.Contains(term))
                .OrderBy(p => p.FirstName)
                .Take(10)
                .ToListAsync();
        }
        public async Task<List<Patient>> GetPatientsAsync()
        {
            return await _context.Patients
                .AsNoTracking()
                .Include(p => p.Visits)
                .OrderBy(p => p.FirstName)
                .ToListAsync();
        }
        public async Task<Patient?> GetPatientDetailAsync(int patientId)
        {
            return await _context.Patients
                .AsNoTracking()
                .Include(p => p.CreatedByUser)
                .Include(p => p.Visits)
                    .ThenInclude(v => v.Doctor)
                .Include(p => p.Visits)
                    .ThenInclude(v => v.PrescribedDrugs)
                        .ThenInclude(pr => pr.Drug)
                .FirstOrDefaultAsync(p => p.PatientId == patientId);
        }
        public async Task<Patient?> GetPatientByIdAsync(int patientId)
        {
            return await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PatientId == patientId);
        }
        public async Task<bool> PatientExistsAsync(int patientId)
        {
            return await _context.Patients
                .AnyAsync(p => p.PatientId == patientId);
        }
        public async Task<bool> CNICExistsAsync(string cnic, int? excludePatientId = null)
        {
            return await _context.Patients
                .AnyAsync(p => p.CNIC == cnic && (!excludePatientId.HasValue || p.PatientId != excludePatientId.Value));
        }
        public async Task CreatePatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
        }
        public void UpdatePatient(Patient patient)
        {
            _context.Patients.Update(patient);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
