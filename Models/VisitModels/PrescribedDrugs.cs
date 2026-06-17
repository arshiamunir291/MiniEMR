using System.Text.Json.Serialization;
using MiniEMR.Enums;
namespace MiniEMR.Models.VisitModels
{
    public class PrescribedDrugs
    {
        [JsonPropertyName("drugId")]
        public int DrugId { get; set; }

        [JsonPropertyName("dosage")]
        public string Dosage { get; set; }= null!;

        [JsonPropertyName("frequency")]
        public Frequency Frequency { get; set; }

        [JsonPropertyName("duration")]
        public short Duration { get; set; } 

        [JsonPropertyName("instructions")]
        public string? Instructions { get; set; }
    }
}
