namespace Hastane.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }
        
        
        public DateTime AppointmentTime { get; set; }
        
        Doctor Doctor { get; set; }
        public int DoctorID { get; set; }
        
        
        Patient Patient { get; set; }
        
        public int PatientID { get; set;}
    }
}
