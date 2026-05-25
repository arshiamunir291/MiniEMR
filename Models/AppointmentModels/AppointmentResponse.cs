namespace MiniEMR.Models.AppointmentModels
{
    public class AppointmentResponse
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public DateOnly Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public DateTime AppointmentDateTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool CanCheckIn { get; set; }
        public bool CanCancel { get; set; }
        public bool CanStartVisit { get; set; }

    }

}
