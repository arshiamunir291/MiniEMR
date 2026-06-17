using Microsoft.EntityFrameworkCore;
using MiniEMR.Data;
using MiniEMR.Entities;
using MiniEMR.Repositories.Interfaces;

namespace MiniEMR.Repositories
{
    public class VisitRepository(AppDbContext _context):IVisitRepository
    {
        public async Task<Appointment?> GetAppointmentForVisitAsync(int appointmentId)
        {
            return await _context.Appointments
                .Include(a=>a.Patient)
                .Include(a=>a.Doctor)
                .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
        }
        public async Task AddVisitAsync(Visit visit)
        {
            await _context.Visits.AddAsync(visit);
        }
        public async Task<List<Drug>> GetDrugsAsync()
        {
            return await _context.Drugs
                .Where(d=>d.IsActive)
                .AsNoTracking()
                .OrderBy(d=>d.DrugName)
                .ToListAsync();
        }
        public async Task<bool> VisitExistsForAppointmentAsync(int appointmentId)
        {
            return await _context.Visits
                .AsNoTracking()
                .AnyAsync(v => v.AppointmentId == appointmentId);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
