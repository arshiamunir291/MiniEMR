using System.Text.Json.Serialization;

namespace MiniEMR.Models.AuthModels
{
    public class LoginRequest
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; } = null!;
        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;
    }
}
