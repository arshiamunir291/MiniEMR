using System.Text.Json.Serialization;

namespace MiniEMR.Models.PatientModels
{
    public class VisitHistory
    {
        [JsonPropertyName("visitId")]
        public int VisitId { get; set; }

        [JsonPropertyName("visitDate")]
        public DateTime VisitDate { get; set; }

        [JsonPropertyName("doctorName")]
        public string DoctorName { get; set; } = null!;

        [JsonPropertyName("vitals")]
        public Vitals Vitals { get; set; } = new();

        [JsonPropertyName("chiefComplaint")]
        public string ChiefComplaint { get; set; } = null!;

        [JsonPropertyName("visitNote")]
        public string VisitNote { get; set; } = null!;

        [JsonPropertyName("diagnosis")]
        public string Diagnosis { get; set; } = null!;

        [JsonPropertyName("prescribedDrug")]
        public List<PrescribedDrug> PrescribedMedicine { get; set; } = [];
    }
}
