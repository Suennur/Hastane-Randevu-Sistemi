using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastane.Models
{
    public class Appointment
    {
        [Key]
        public int AppoId { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        [Display(Name ="Doctor")]
        public Doctor? doctor { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [Display(Name ="Patient")]
        public Patient? patient { get; set; }
    }
}
