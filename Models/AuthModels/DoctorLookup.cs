using System.Text.Json.Serialization;

namespace MiniEMR.Models.AuthModels
{
    public class DoctorLookup
    {
        [JsonPropertyName("id")]
        public int UserId { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;
    }
}
