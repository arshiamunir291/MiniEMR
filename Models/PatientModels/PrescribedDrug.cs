using System.Text.Json.Serialization;
using MiniEMR.Enums;

namespace MiniEMR.Models.PatientModels
{
    public class PrescribedDrug
    {
        [JsonPropertyName("drugId")]
        public int Id { get; set; }

        [JsonPropertyName("drugName")]
        public string DrugName { get; set; } = null!;

        [JsonPropertyName("dosage")]
        public string Dosage { get; set; } = null!;

        [JsonPropertyName("frequency")]
        public Frequency Frequency { get; set; }

        [JsonPropertyName("duration")]
        public short Duration { get; set; } 

        [JsonPropertyName("instructions")]
        public string? Instructions { get; set; }
    }
}
