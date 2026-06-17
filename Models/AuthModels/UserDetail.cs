using System.Text.Json.Serialization;

namespace MiniEMR.Models.AuthModels
{
    public class UserDetail
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; } = null!;

        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = null!;

        [JsonPropertyName("role")]
        public string Role { get; set; } = null!;

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("specialization")]
        public string? Specialization { get; set; } 

    }
}
