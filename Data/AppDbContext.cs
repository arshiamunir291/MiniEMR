using Microsoft.EntityFrameworkCore;
using MiniEMR.Entities;
namespace MiniEMR.Data
{
    public partial class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<PrescribedDrug> PrescribedDrugs { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<Drug> Drugs { get; set; }
        public virtual DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }

}
