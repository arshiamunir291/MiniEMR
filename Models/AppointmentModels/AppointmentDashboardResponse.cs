namespace MiniEMR.Models.AppointmentModels
{
    public class AppointmentDashboardResponse
    {
        public DashboardSummary Summary { get; set; } = new();
        public List<AppointmentResponse> Appointments { get; set; } = new();
        public List<AppointmentResponse> MyTodayAppointments { get; set; }= new();
    }
}
