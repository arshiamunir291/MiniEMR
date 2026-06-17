using System.Text.Json.Serialization;

namespace MiniEMR.Models.PatientModels
{
    public class PatientDetail
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("mrn")]
        public string MRN { get; set; } = null!;

        [JsonPropertyName("firstName")]

        public string FirstName { get; set; } = null!;

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = null!;

        [JsonPropertyName("dateOfBirth")]
        public DateOnly DateOfBirth { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; } = null!;

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; } = null!;

        [JsonPropertyName("bloodGroup")]
        public string? BloodGroup { get; set; }

        [JsonPropertyName("cnic")]
        public string? CNIC { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; } = null!;

        [JsonPropertyName("emergencyContactName")]
        public string? EmergencyContactName { get; set; }

        [JsonPropertyName("emergencyContactNumber")]
        public string? EmergencyContactNumber { get; set; }

        [JsonPropertyName("allergies")]
        public string? Allergies { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; } = null!;

        [JsonPropertyName("lastVisitDate")]
        public DateTime? LastVisitDate { get; set; }

        [JsonPropertyName("visits")]
        public List<VisitHistory> Visits { get; set; } = [];
    }
}
