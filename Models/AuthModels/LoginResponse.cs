using System.Text.Json.Serialization;

namespace MiniEMR.Models.AuthModels
{
    public class LoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = null!;
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = null!;
        [JsonPropertyName("role")]
        public string Role { get; set; } = null!;

    }
}
