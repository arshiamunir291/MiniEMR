using System.Text.Json.Serialization;

namespace MiniEMR.Models.AppointmentModels
{
    public class DashboardSummary
    {
        [JsonPropertyName("totalAppointment")]

        public int TotalAppointments { get; set; }

        [JsonPropertyName("scheduled")]
        public int Scheduled { get; set; }

        [JsonPropertyName("checkedIn")]
        public int CheckedIn { get; set; }

        [JsonPropertyName("completed")]
        public int Completed { get; set; }
    }
}
