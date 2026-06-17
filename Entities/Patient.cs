using MiniEMR.Enums;

namespace MiniEMR.Entities
{
    public class Patient
    {
        public int PatientId { get; set; } 
        public string MRN { get; set; } = null!; 
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; } 
        public string CNIC { get; set; } = null!; 
        public string PhoneNumber { get; set; } = null!;
        public string? BloodGroup { get; set; }
        public string Address { get; set; } = null!;
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public string? Allergies { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public User CreatedByUser { get; set; } = null!;
        public User? UpdatedByUser { get; set; }
        public ICollection<Visit> Visits { get; set; }= new List<Visit>();

    }
}
