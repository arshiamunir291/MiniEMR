using MiniEMR.Data;
using MiniEMR.Entities;
using MiniEMR.Enums;
using MiniEMR.Models.VisitModels;
using MiniEMR.Repositories.Interfaces;
using MiniEMR.Services.Interfaces;

namespace MiniEMR.Services
{
    public class VisitService(
        IVisitRepository visitRepository,
        AppDbContext context,
        IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository,
        IAuthRepository authRepository)
        : IVisitService
    {
        public async Task<StartVisit> StartVisitAsync(int appointmentId, int currentDoctorId)
        {
            var appointment = await appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }

            if (appointment.Status != AppointmentStatus.CheckedIn)
            {
                throw new Exception(
                    "Only checked-in appointments can start visits");
            }
            var allowedVisitStartTime = appointment.AppointmentDateTime.AddMinutes(-15);
            var appointmentExpiryTime = appointment.AppointmentDateTime.AddHours(2);
            if (DateTime.Now< allowedVisitStartTime)
            {
                throw new Exception(
                    "Visit can only start 15 minutes before appointment time.");
            }

            if (DateTime.Now> appointmentExpiryTime)
            {
                appointment.Status = AppointmentStatus.Cancelled;
                appointment.CancellationReason = "Appointment expired after 2 hours.";
                appointment.UpdatedAt = DateTime.Now;
                await appointmentRepository.UpdateAsync(appointment);
                await visitRepository.SaveChangesAsync();
                throw new Exception("Appointment has expired and was automatically cancelled.");
            }

            var patient = await patientRepository.GetPatientByIdAsync(appointment.PatientId);
            if (patient == null)
            {
                throw new Exception("Patient not found");
            }
            var doctor = await authRepository.GetByUserIdAsync(currentDoctorId);
            if (doctor == null)
            {
                throw new Exception("Doctor not found");
            }

            return new StartVisit
            {
                AppointmentId = appointment.AppointmentId,
                PatientId = patient.PatientId,
                PatientName = patient.FirstName + " " + patient.LastName,
                DoctorId = doctor.UserId,
                DoctorName = doctor.FullName,
                AppointmentDate = appointment.AppointmentDateTime,
                VisitType = VisitType.Appointment
            };
        }

        public async Task<StartVisit> StartEmergencyVisitAsync(int patientId, int currentDoctorId)

        {
            var patient = await patientRepository.GetPatientByIdAsync(patientId);
            if (patient == null)
            {
                throw new Exception("Patient not found");
            }

            var doctor = await authRepository.GetByUserIdAsync(currentDoctorId);
            if (doctor == null)
            {
                throw new Exception("Doctor not found");
            }

            return new StartVisit
            {
                AppointmentId = null,
                PatientId = patient.PatientId,
                PatientName = patient.FirstName + " " + patient.LastName,
                DoctorId = doctor.UserId,
                DoctorName = doctor.FullName,
                AppointmentDate = DateTime.Now,
                VisitType = VisitType.Emergency
            };
        }

        public async Task<List<DrugDropDown>> GetDrugsAsync()
        {
            var drugs = await visitRepository.GetDrugsAsync();
            return drugs.Select(d => new DrugDropDown
            {
                DrugId = d.DrugId,
                DrugName = d.DrugName,
                Strength = d.Strength
            }).ToList();
        }

        public async Task SaveVisitAsync(SaveVisit model, int currentDoctorId)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                Appointment? appointment = null;

                if (model.VisitType == VisitType.Appointment)
                {
                    if (!model.AppointmentId.HasValue)
                    {
                        throw new Exception("AppointmentId is required for appointment visits.");

                    }

                    appointment = await visitRepository.GetAppointmentForVisitAsync(model.AppointmentId.Value);
                    if (appointment == null)
                    {
                        throw new Exception("Appointment not found.");

                    }

                    if (appointment.DoctorId != currentDoctorId)
                    {
                        throw new Exception("You are not authorized to start this visit.");

                    }

                    if (appointment.Status != AppointmentStatus.CheckedIn)
                    {
                        throw new Exception("Only checked-in appointments can start visits.");

                    }

                    var allowedVisitStartTime = appointment.AppointmentDateTime.AddMinutes(-15);
                    var appointmentExpiryTime = appointment.AppointmentDateTime.AddHours(2);
                    if (DateTime.Now < allowedVisitStartTime)
                    {
                        throw new Exception("Visit can only start 15 minutes before appointment time.");

                    }

                    if (DateTime.Now> appointmentExpiryTime)
                    {
                        appointment.Status = AppointmentStatus.Cancelled;
                        appointment.CancellationReason = "Appointment expired after 2 hours.";
                        appointment.UpdatedAt = DateTime.Now;
                        await appointmentRepository.UpdateAsync(appointment);
                        await visitRepository.SaveChangesAsync();
                        throw new Exception("Appointment has expired and was automatically cancelled.");

                    }
                    bool visitExists = await visitRepository.VisitExistsForAppointmentAsync(appointment.AppointmentId);
                    if (visitExists)
                    {
                        throw new Exception("Visit already exists for this appointment.");

                    }
                }
                else if (model.VisitType == VisitType.Emergency)
                {
                    if (model.PatientId <= 0)
                    {
                        throw new Exception("PatientId is required for emergency visits.");

                    }

                    var patient = await patientRepository.GetPatientByIdAsync(model.PatientId);
                    if (patient == null)
                    {
                        throw new Exception("Patient not found.");
                    }
                }
                var visit = new Visit
                {
                    AppointmentId = model.VisitType == VisitType.Appointment ? model.AppointmentId : null,
                    PatientId = appointment != null ? appointment.PatientId : model.PatientId,
                    DoctorId = currentDoctorId,
                    VisitType = model.VisitType,
                    Height = model.Height,
                    Weight = model.Weight,
                    BpSystolic = model.BpSystolic,
                    BpDiastolic = model.BpDiastolic,
                    PulseRate = model.PulseRate,
                    Temperature = model.Temperature,
                    RespiratoryRate = model.RespiratoryRate,
                    BMI = model.BMI,
                    ChiefComplaint = model.ChiefComplaint,
                    VisitsNotes = model.VisitsNotes,
                    Diagnosis = model.Diagnosis,
                    FollowUpInstructions = model.FollowUpInstructions,
                    CreatedAt = DateTime.Now,
                };

                if (model.PrescribedDrugs != null)
                {
                    foreach (var prescribedDrug in model.PrescribedDrugs)

                    {
                        visit.PrescribedDrugs.Add(
                            new PrescribedDrug
                            {
                                DrugId = prescribedDrug.DrugId,
                                Dosage = prescribedDrug.Dosage,
                                Frequency = prescribedDrug.Frequency,
                                Duration = prescribedDrug.Duration,
                                Instructions = prescribedDrug.Instructions
                            });

                    }
                }

                await visitRepository.AddVisitAsync(visit);
                if (appointment != null)
                {
                    appointment.Status = AppointmentStatus.Completed;
                    appointment.UpdatedAt = DateTime.Now;
                    await appointmentRepository.UpdateAsync(appointment);
                }
                await visitRepository.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}