using Microsoft.EntityFrameworkCore;
using MiniEMR.Entities;
using MiniEMR.Enums;
using MiniEMR.Models.AppointmentModels;
using MiniEMR.Repositories.Interfaces;
using MiniEMR.Services.Interfaces;

namespace MiniEMR.Services
{
    public class AppointmentService(IAppointmentRepository appointmentRepository,IAuthRepository authRepository):IAppointmentService
    {
        public async Task<AppointmentDashboardResponse> GetAppointmentsAsync(DateTime date, AppointmentStatus? status, int userId)
        {
            await AutoExpireAppointmentsAsync();
            var currentUser=await authRepository.GetByUserIdAsync(userId);
            if(currentUser == null)
            {
                throw new UnauthorizedAccessException("Invalid User.");
            }
            var appointments=await appointmentRepository.GetAppointmentsByDateAsync(date, status);
            var summaryAppointments=await appointmentRepository.GetAppointmentsForSummaryAsync(date);
            var response = new AppointmentDashboardResponse
            {
                Summary = BuildSummary(summaryAppointments),
                Appointments = appointments
                .Select(appointments => 
                 MapToAppointmentResponse(appointments, currentUser))
                .ToList(),
            };
            if(currentUser.Role == UserRole.Doctor)
            {
                var todayAppointments=await appointmentRepository.GetDoctorTodayAppointmentsAsync(currentUser.UserId, DateTime.Today);
                response.MyTodayAppointments=todayAppointments
                    .Select(appointment =>
                     MapToAppointmentResponse(appointment, currentUser))
                    .ToList();
                    
            }
            return response;
        }
        private DashboardSummary BuildSummary(List<Appointment> appointments)

        {
            return new DashboardSummary
            {
                TotalAppointments = appointments.Count,
                Scheduled = appointments.Count(a => a.Status == AppointmentStatus.Scheduled),
                CheckedIn = appointments.Count(a => a.Status == AppointmentStatus.CheckedIn),
                Completed = appointments.Count(a => a.Status == AppointmentStatus.Completed),

            };
        }

        private AppointmentResponse MapToAppointmentResponse(Appointment appointment, User currentUser)

        {
            var isCurrentDoctorAppointment = currentUser.Role == UserRole.Doctor && appointment.DoctorId == currentUser.UserId;
            return new AppointmentResponse
            {
                AppointmentId = appointment.AppointmentId,
                PatientId = appointment.PatientId,
                PatientName = $"{appointment.Patient.FirstName} {appointment.Patient.LastName}",
                Age = appointment.Patient.DateOfBirth,
                Gender = appointment.Patient.Gender.ToString(),
                DoctorId = appointment.DoctorId,
                DoctorName = appointment.Doctor.FullName,
                AppointmentDateTime = appointment.AppointmentDateTime,
                Status = appointment.Status.ToString(),
                CanCheckIn = appointment.Status == AppointmentStatus.Scheduled,
                CanCancel = appointment.Status == AppointmentStatus.Scheduled || appointment.Status == AppointmentStatus.CheckedIn,
                CanStartVisit = appointment.Status == AppointmentStatus.CheckedIn && isCurrentDoctorAppointment
            };
        }
        public async Task<AppointmentResponse> CreateAppointmentAsync(CreateAppointment request, int createdByUserId)
        {
            if (request.AppointmentDateTime <= DateTime.Now)
            {
                throw new Exception(
                    "Cannot book an appointment in the past."
                );
            }

            try
            {
                var appointment = new Appointment
                {
                    PatientId = request.PatientId,
                    DoctorId = request.DoctorId,
                    AppointmentDateTime = request.AppointmentDateTime,
                    Notes = request.Notes,
                    Status = AppointmentStatus.Scheduled,
                    CreatedAt = DateTime.Now,
                    CreatedBy = createdByUserId
                };
                await appointmentRepository.AddAsync(appointment);
                await appointmentRepository.SaveChangesAsync();
                return new AppointmentResponse
                {
                    AppointmentId = appointment.AppointmentId,
                    Status = appointment.Status.ToString()
                };
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains(
                    "UX_Appointments_Doctor_DateTime_Active") == true)
                {
                    throw new Exception(
                        "Doctor already has an appointment at this time.");
                }

                throw;
            }
        }
        public async Task CheckInAppointmentAsync(int appointmentId, int updatedByUserId)

        {
            var appointment = await appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                throw new Exception("Appointment not found.");
            }

            if (appointment.Status != AppointmentStatus.Scheduled)
            {
                throw new Exception(
                    "Only scheduled appointments can be checked in.");
            }

            appointment.Status = AppointmentStatus.CheckedIn;
            appointment.UpdatedAt = DateTime.Now;
            appointment.UpdatedBy = updatedByUserId;
            await appointmentRepository.SaveChangesAsync();
        }
        public async Task CancelAppointmentAsync(int appointmentId, CancelAppointmentRequest request, int updatedByUserId)

        {
            var appointment = await appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                throw new Exception("Appointment not found.");
            }

            if (appointment.Status == AppointmentStatus.Cancelled)
            {
                throw new Exception(
                    "Appointment is already cancelled.");
            }

            appointment.Status = AppointmentStatus.Cancelled;
            appointment.CancellationReason = request.CancellationReason;
            appointment.UpdatedAt = DateTime.Now;
            appointment.UpdatedBy = updatedByUserId;
            await appointmentRepository.UpdateAsync(appointment);
            await appointmentRepository.SaveChangesAsync();
        }
        private async Task AutoExpireAppointmentsAsync()
        {
            var expiredAppointments = await appointmentRepository.GetExpiredAppointmentsAsync();
            foreach (var appointment in expiredAppointments)

            {
                appointment.Status = AppointmentStatus.Cancelled;
                appointment.CancellationReason = "Patient not came.";
                appointment.UpdatedAt = DateTime.Now;
            }
            if (expiredAppointments.Any())
            {
                await appointmentRepository.SaveChangesAsync();

            }
        }
        public async Task<List<string>> GetAvailableSlotsAsync(int doctorId, DateTime date)
        {
            return await appointmentRepository.GetAvailableSlotsAsync(doctorId, date);

        }

    }    
}

