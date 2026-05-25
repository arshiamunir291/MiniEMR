using System.Text.Json.Serialization;

namespace MiniEMR.Models.AppointmentModels
{
    public class CreateAppointment
    {
        [JsonPropertyName("patientId")]
        public int PatientId { get; set; }

        [JsonPropertyName("doctorId")]
        public int DoctorId { get; set; }

        [JsonPropertyName("appointmentDateTime")]

        public DateTime AppointmentDateTime { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }
    }
}
