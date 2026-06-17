using System.Text.Json.Serialization;

namespace MiniEMR.Models.PatientModels
{
    public class Vitals
    {
        [JsonPropertyName("height")]
        public decimal? Height { get; set; }

        [JsonPropertyName("weight")]
        public decimal? Weight { get; set; }

        [JsonPropertyName("bpSystolic")]
        public int? BloodPressureSystolic { get; set; }

        [JsonPropertyName("bpDiastolic")]
        public int? BloodPressureDiastolic { get; set; }

        [JsonPropertyName("pulse")]
        public int? Pulse { get; set; }

        [JsonPropertyName("temperature")]
        public decimal? Temperature { get; set; }

        [JsonPropertyName("respiratoryRate")]
        public int? RespiratoryRate { get; set; }
    }
}
