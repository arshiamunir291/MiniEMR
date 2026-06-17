using System.Text.Json.Serialization;
using MiniEMR.Enums;

namespace MiniEMR.Models.PatientModels
{
    public class PatientList
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("mrn")]
        public string MRN { get; set; } = null!;

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }= null!;

        [JsonPropertyName("gender")]
        public string Gender { get; set; } = null!;

        [JsonPropertyName("dateOfBirth")]
        public DateOnly DateOfBirth { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }= null!;

        [JsonPropertyName("bloodGroup")]
        public string? BloodGroup { get; set; }

        [JsonPropertyName("lastVisitDate")]
        public DateTime? LastVisitDate { get; set; }

    }
}
