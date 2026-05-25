using MiniEMR.Data;
using MiniEMR.Enums;

namespace MiniEMR.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string? Notes { get; set; }
        public string? CancellationReason { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public Patient Patient { get; set; } = null!;
        public User Doctor { get; set; } = null!;
        public User CreatedByUser { get; set; } = null!;
        public User? UpdatedByUser { get; set; }
        public Visit? Visit { get; set; }
    }
}
