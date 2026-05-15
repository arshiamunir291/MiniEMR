namespace MiniEMR.Entities
{
    public class Visit
    {
        public int VisitId { get; set; }
        public int? AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string VisitType { get; set; } = null!;

        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        public short? BpSystolic { get; set; }

        public short? BpDiastolic { get; set; }

        public short? PulseRate { get; set; }

        public decimal? Temperature { get; set; }

        public short? RespiratoryRate { get; set; }

        public decimal? BMI { get; set; }

        public string ChiefComplaint { get; set; } = null!;

        public string VisitsNotes { get; set; } = null!;

        public string Diagnosis { get; set; } = null!;

        public string? FollowUpInstructions { get; set; }
        public DateTime CreatedAt { get; set; }
        public Appointment? Appointment { get; set; }

        public Patient Patient { get; set; } = null!;

        public User Doctor { get; set; } = null!;

        public ICollection<Prescription> Prescriptions { get; set; }
            = new List<Prescription>();


    }
}
