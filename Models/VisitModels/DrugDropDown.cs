using System.Text.Json.Serialization;

namespace MiniEMR.Models.VisitModels
{
    public class DrugDropDown
    {
        [JsonPropertyName("drugId")]
        public int DrugId { get; set; }

        [JsonPropertyName("drugName")]
        public string DrugName { get; set; } = null!;

        [JsonPropertyName("strength")]
        public string? Strength { get; set; }
    }
}
