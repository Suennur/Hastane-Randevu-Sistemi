using Microsoft.EntityFrameworkCore;
namespace Hastane.Models
{
    public class HospitalDataContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Policlinic> Policlinics { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public HospitalDataContext(DbContextOptions<HospitalDataContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HospitalDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        
        public HospitalDataContext()
        {
        }
    }
}
