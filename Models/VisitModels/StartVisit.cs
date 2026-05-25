using System.Text.Json.Serialization;
using MiniEMR.Enums;

namespace MiniEMR.Models.VisitModels
{
    public class StartVisit
    {
        [JsonPropertyName("appointmentId")]
        public int? AppointmentId { get; set; }

        [JsonPropertyName("patientId")]
        public int PatientId { get; set; }

        [JsonPropertyName("patientName")]
        public string PatientName { get; set; } = null!;

        [JsonPropertyName("doctorId")]
        public int DoctorId { get; set; }

        [JsonPropertyName("doctorName")]
        public string DoctorName { get; set; } = null!;

        [JsonPropertyName("appointmentdate")]
        public DateTime AppointmentDate { get; set; }

        [JsonPropertyName("visitType")]
        public VisitType VisitType { get; set; } 
    }
}
