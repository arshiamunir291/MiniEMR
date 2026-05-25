using System.Text.Json.Serialization;

namespace MiniEMR.Models.AppointmentModels
{
    public class CancelAppointmentRequest
    {
        [JsonPropertyName("cancellationReason")]
        public string? CancellationReason { get; set; }
    }
}
