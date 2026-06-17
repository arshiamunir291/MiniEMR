using System.Text.Json.Serialization;
using MiniEMR.Entities;
using MiniEMR.Enums;

namespace MiniEMR.Models.VisitModels
{
    public class SaveVisit
    {
        [JsonPropertyName("appointmentId")]
        public int? AppointmentId { get; set; }

        [JsonPropertyName("patientId")]
        public int PatientId { get; set; }
        [JsonPropertyName("visitType")]
        public VisitType VisitType { get; set; }

        [JsonPropertyName("height")]
        public decimal? Height { get; set; }

        [JsonPropertyName("weight")]
        public decimal? Weight { get; set; }

        [JsonPropertyName("bpSystolic")]
        public short? BpSystolic { get; set; }

        [JsonPropertyName("bpDiastolic")]
        public short? BpDiastolic { get; set; }

        [JsonPropertyName("pulseRate")]
        public short? PulseRate { get; set; }

        [JsonPropertyName("temperature")]
        public decimal? Temperature { get; set; }

        [JsonPropertyName("respiratoryRate")]
        public short? RespiratoryRate { get; set; }

        [JsonPropertyName("bmi")]
        public decimal? BMI { get; set; }

        [JsonPropertyName("chiefComplaint")]
        public string ChiefComplaint { get; set; } = null!;

        [JsonPropertyName("visitsNotes")]
        public string VisitsNotes { get; set; } = null!;

        [JsonPropertyName("diagnosis")]
        public string Diagnosis { get; set; } = null!;

        [JsonPropertyName("followUpInstructions")]
        public string? FollowUpInstructions { get; set; }
   
        [JsonPropertyName("prescribedDrugs")]
        public List<PrescribedDrugs>? PrescribedDrugs { get; set; }
    }
}
