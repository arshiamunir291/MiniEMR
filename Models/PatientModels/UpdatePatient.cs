using System.Text.Json.Serialization;
using MiniEMR.Enums;

namespace MiniEMR.Models.PatientModels
{
    public class UpdatePatient
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = null!;

        [JsonPropertyName("lastname")]
        public string LastName { get; set; } = null!;

        [JsonPropertyName("dateOfBirth")]
        public DateOnly DateOfBirth { get; set; }

        [JsonPropertyName("gender")]
        public Gender Gender { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; } = null!;

        [JsonPropertyName("cnic")]
        public string CNIC { get; set; } = null!;

        [JsonPropertyName("bloodGroup")]
        public string BloodGroup { get; set; } = null!;

        [JsonPropertyName("address")]
        public string Address { get; set; }= null!;

        [JsonPropertyName("allergies")]
        public string? Allergies { get; set; }

        [JsonPropertyName("emergencyContactNumber")]
        public string EmergencyContactNumber { get; set; } = null!;

        [JsonPropertyName("emergencyContactName")]
        public string EmergencyContactName { get; set; } = null!;
    }
}
