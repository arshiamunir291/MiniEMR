namespace MiniEMR.Models.AuthModels
{
    public class UserDetail
    {
        public string UserName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Specialization { get; set; } 

    }
}
