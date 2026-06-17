using Microsoft.EntityFrameworkCore;
using MiniEMR.Data;
using MiniEMR.Entities;
using MiniEMR.Enums;
using MiniEMR.Repositories.Interfaces;

namespace MiniEMR.Repositories
{
    public class AppointmentRepository(AppDbContext context) : IAppointmentRepository
    {
        public async Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime date, AppointmentStatus? status)
        {
            var query = context.Appointments
                .AsNoTracking()
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .AsQueryable();
            query = query.Where(a => a.AppointmentDateTime.Date == date.Date);
            if (status.HasValue)
            {
                query = query.Where(a => a.Status == status.Value);
            }
            return await query.OrderBy(a => a.AppointmentDateTime).ToListAsync();
        }
        public async Task<List<Appointment>> GetDoctorTodayAppointmentsAsync(int doctorId, DateTime today)
        {
            return await context.Appointments.AsNoTracking()
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.DoctorId == doctorId && a.AppointmentDateTime.Date == today.Date)
                .OrderBy(a => a.AppointmentDateTime)
                .ToListAsync();
        }
        public async Task<List<Appointment>> GetAppointmentsForSummaryAsync(DateTime date)
        {
            return await context.Appointments.AsNoTracking()
                .AsNoTracking()
                .Where(a => a.AppointmentDateTime.Date == date.Date)
                .ToListAsync();
        }
        public async Task AddAsync(Appointment appointment)
        {
            await context.Appointments.AddAsync(appointment);

        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
        public async Task<Appointment?> GetByIdAsync(int appointmentId)
        {
            return await context.Appointments
                .FirstOrDefaultAsync(x =>
                    x.AppointmentId == appointmentId);
        }
        public async Task UpdateAsync(Appointment appointment)
        {
            context.Appointments.Update(appointment);

            await Task.CompletedTask;
        }

        public async Task<List<Appointment>> GetExpiredAppointmentsAsync()
        {
            return await context.Appointments
      .Where(a =>

          a.Status ==
              AppointmentStatus.Scheduled &&

          DateTime.Now >
              a.AppointmentDateTime.AddHours(2)

      )
      .ToListAsync();
        }
        public async Task<List<string>> GetAvailableSlotsAsync(int doctorId, DateTime date)

        {
            var dayOfWeek =
                date.DayOfWeek;

            var schedule =
                await context.DoctorSchedules
                    .FirstOrDefaultAsync(x =>

                        x.DoctorId == doctorId &&
                        x.DayOfWeek == dayOfWeek &&
                        x.IsActive
                    );

            if (schedule == null)
            {
                return new List<string>();
            }

            var bookedAppointments =
                await context.Appointments
                    .Where(x =>

                        x.DoctorId == doctorId &&

                        x.AppointmentDateTime.Date
                            == date.Date &&

                        x.Status !=
                            AppointmentStatus.Cancelled
                    )
                    .Select(x =>
                        x.AppointmentDateTime.TimeOfDay
                    )
                    .ToListAsync();

            var slots = new List<string>();
            var current = schedule.StartTime;
            while (current < schedule.EndTime)
            {
                var slotEnd =
                    current.Add(
                        TimeSpan.FromMinutes(
                            schedule.SlotDurationMinutes
                        )
                    );
                var isBreakTime =
                    schedule.BreakStartTime.HasValue &&
                    schedule.BreakEndTime.HasValue &&
                    current >= schedule.BreakStartTime &&
                    current < schedule.BreakEndTime;
                var isBooked = bookedAppointments.Contains(current);
                var isPastSlot =
                date.Date == DateTime.Today &&
                current <= DateTime.Now.TimeOfDay;
                if (!isBreakTime &&
                    !isBooked &&
                    !isPastSlot)
                {
                    slots.Add(current.ToString(@"hh\:mm"));
                }
                current = slotEnd;

            }
            return slots;
        }
    }
}
