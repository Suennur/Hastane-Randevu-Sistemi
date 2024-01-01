using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Reflection.Metadata.BlobBuilder;

namespace Hastane.Models
{
    public class Doctor
    {
        [Key]
        public int doctorId { get; set; }

        [Required(ErrorMessage ="This field required")]
        [Display(Name ="Doctor")]
        public string? doctorName{ get; set; }

        [Required(ErrorMessage = "This field required")]
        [Display(Name = "Policlinic")]
        public int polic { get; set; }

        [Required(ErrorMessage = "This field required")]
        [Display(Name = "Work Time")]
        public DateTime workTime { get; set; }


        [ForeignKey("polic")]
        public Policlinic policlinic { get; set; }

        public ICollection<Appointment> Appointments{ get; set; }
    }
}
