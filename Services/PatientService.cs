using MiniEMR.Entities;
using MiniEMR.Enums;
using MiniEMR.Models.PatientModels;
using MiniEMR.Repositories.Interfaces;
using MiniEMR.Services.Interfaces;

namespace MiniEMR.Services
{
    public class PatientService(IPatientRepository patientRepository)
        : IPatientService
    {
        public async Task<List<PatientLookup>> SearchPatientsAsync(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return [];
            }

            var patients = await patientRepository.SearchPatientsAsync(term);

            return patients.Select(p => new PatientLookup
            {
                Id = p.PatientId,

                FullName = $"{p.FirstName} {p.LastName}"

            }).ToList();
        }

        public async Task<List<PatientList>> GetPatientsAsync()
        {
            var patients = await patientRepository.GetPatientsAsync();

            return patients.Select(p => new PatientList
            {
                Id = p.PatientId,

                MRN = p.MRN,

                FullName = $"{p.FirstName} {p.LastName}",

                Gender = p.Gender.ToString(),

                DateOfBirth = p.DateOfBirth,

                PhoneNumber = p.PhoneNumber,

                BloodGroup = p.BloodGroup,

                LastVisitDate = p.Visits?
                    .OrderByDescending(v => v.CreatedAt)
                    .FirstOrDefault()?.CreatedAt

            }).ToList();
        }

        public async Task<PatientDetail?> GetPatientDetailAsync(int patientId)
        {
            var patient = await patientRepository
                .GetPatientDetailAsync(patientId);

            if (patient == null)
            {
                return null;
            }

            return new PatientDetail
            {
                Id = patient.PatientId,

                MRN = patient.MRN,

                FirstName = patient.FirstName,

                LastName = patient.LastName,

                DateOfBirth = patient.DateOfBirth,

                Gender = patient.Gender.ToString(),

                PhoneNumber = patient.PhoneNumber,

                BloodGroup = patient.BloodGroup,

                CNIC = patient.CNIC,

                Address = patient.Address,

                EmergencyContactName = patient.EmergencyContactName,

                EmergencyContactNumber = patient.EmergencyContactNumber,

                Allergies = patient.Allergies,

                CreatedBy = patient.CreatedByUser?.FullName ?? string.Empty,

                LastVisitDate = patient.Visits?
                    .OrderByDescending(v => v.CreatedAt)
                    .FirstOrDefault()?.CreatedAt,

                Visits = patient.Visits?
                    .OrderByDescending(v => v.CreatedAt)
                    .Select(v => new VisitHistory
                    {
                        VisitId = v.VisitId,

                        VisitDate = v.CreatedAt,

                        DoctorName = v.Doctor?.FullName ?? string.Empty,

                        ChiefComplaint = v.ChiefComplaint,

                        VisitNote = v.VisitsNotes,

                        Diagnosis = v.Diagnosis,

                        Vitals = new Vitals
                        {
                            Height = v.Height,

                            Weight = v.Weight,

                            BloodPressureSystolic = v.BpSystolic,

                            BloodPressureDiastolic = v.BpDiastolic,

                            Pulse = v.PulseRate,

                            Temperature = v.Temperature,

                            RespiratoryRate = v.RespiratoryRate
                        },

                        PrescribedMedicine = v.PrescribedDrugs?
                            .Select(pr => new Models.PatientModels.PrescribedDrug
                            {
                                Id = pr.DrugId,

                                DrugName = pr.Drug?.DrugName ?? string.Empty,

                                Dosage = pr.Dosage,

                                Frequency = pr.Frequency,

                                Duration = pr.Duration,

                                Instructions = pr.Instructions

                            }).ToList() ?? []

                    }).ToList() ?? []
            };
        }

        public async Task CreatePatientAsync(CreatePatient model, int userId)


        {
            if (!string.IsNullOrWhiteSpace(model.CNIC))
            {
                var cnicExists = await patientRepository
                    .CNICExistsAsync(model.CNIC);

                if (cnicExists)
                {
                    throw new Exception("CNIC already exists.");
                }
            }

            var patient = new Patient
            {
                MRN = GenerateMRN(),

                FirstName = model.FirstName,

                LastName = model.LastName,

                DateOfBirth = model.DateOfBirth,

                Gender = model.Gender,

                PhoneNumber = model.PhoneNumber,

                CNIC = model.CNIC,

                BloodGroup = model.BloodGroup,

                Address = model.Address,

                EmergencyContactName = model.EmergencyContactName,

                EmergencyContactNumber = model.EmergencyContactNumber,

                Allergies = model.Allergies,

                CreatedAt = DateTime.UtcNow,
                CreatedBy=userId
            };

            await patientRepository.CreatePatientAsync(patient);

            await patientRepository.SaveChangesAsync();
        }

        public async Task UpdatePatientAsync(UpdatePatient model)
        {
            var patient = await patientRepository.GetPatientByIdAsync(model.Id);

            if (patient == null)
            {
                throw new Exception("Patient not found.");
            }

            if (!string.IsNullOrWhiteSpace(model.CNIC))
            {
                var cnicExists = await patientRepository
                    .CNICExistsAsync(model.CNIC, model.Id);

                if (cnicExists)
                {
                    throw new Exception("CNIC already exists.");
                }
            }
            patient.FirstName = model.FirstName;
            patient.LastName = model.LastName;
            patient.DateOfBirth = model.DateOfBirth;
            patient.Gender = model.Gender;
            patient.PhoneNumber = model.PhoneNumber;
            patient.CNIC = model.CNIC;
            patient.BloodGroup = model.BloodGroup;
            patient.Address = model.Address;
            patient.EmergencyContactName = model.EmergencyContactName;
            patient.EmergencyContactNumber = model.EmergencyContactNumber;
            patient.Allergies = model.Allergies;
            patient.UpdatedAt = DateTime.UtcNow;
            patientRepository.UpdatePatient(patient);
            await patientRepository.SaveChangesAsync();
        }

        private string GenerateMRN()
        {
            return $"MRN-{DateTime.UtcNow:yyyyMMddHHmmss}";
        }
    }
}