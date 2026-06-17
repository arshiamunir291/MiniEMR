using System.Text.Json.Serialization;

namespace MiniEMR.Models.AppointmentModels
{
    public class CreateAppointmentResponse
    {
        [JsonPropertyName("appointmentId")]
        public int AppointmentId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}
