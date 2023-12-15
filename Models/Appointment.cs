namespace Hastane.Models
{
    public class Appointment
    {
        public int AppoID { get; set; }

        public DateTime AppoTime { get; set; }

        Doctor DoctorID { get; set; }

        Patient PatientID { get; set; }
    }
}
