using System.ComponentModel.DataAnnotations;
using static System.Reflection.Metadata.BlobBuilder;

namespace Hastane.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required(ErrorMessage ="This field required.")]
        [Display(Name = "Name")]
        public string? PatientName { get; set; }

        [Required(ErrorMessage = "This field required.")]
        [Display(Name = "Surname")]
        public string? PatientSurname { get; set; }

        [Required(ErrorMessage ="This field requried")]
        [Display(Name = "TC Number")]
        public string? tc { get; set; }

        [Required(ErrorMessage ="This field required.")]
        [Display(Name ="Password")]
        [MinLength(3)]
        public string? PatientPassword { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
