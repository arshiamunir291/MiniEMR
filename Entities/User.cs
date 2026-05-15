using MiniEMR.Enums;

namespace MiniEMR.Entities
{
    public class User
    {
        public int UserId { get; set; } 
        public UserRole Role { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Specialization { get; set; } // For doctors
        public bool IsActive { get; set; } 
    }
}
