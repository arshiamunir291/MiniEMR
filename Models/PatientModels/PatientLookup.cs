using System.Text.Json.Serialization;

namespace MiniEMR.Models.PatientModels
{
    public class PatientLookup
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = null!;
    }
}
