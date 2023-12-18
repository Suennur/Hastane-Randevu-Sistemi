using System.ComponentModel.DataAnnotations;
namespace Hastane.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

        [MinLength(2, ErrorMessage = "Minimum 2 karakter")]
        [MaxLength(25, ErrorMessage = "Maksimum 25 karakter")]
        [Required(ErrorMessage ="Bu alan zorunludur")]
        [Display(Name ="AD")]
        public string PatientFName { get; set; } = string.Empty;

        [MinLength(2, ErrorMessage = "Minimum 2 karakter")]
        [MaxLength(25, ErrorMessage = "Maksimum 25 karakter")]
        [Required(ErrorMessage = "Bu alan zorunludur")]
        [Display(Name = "SOYAD")]
        public string PatientLName { get; set; }

        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Geçerli bir TC Kimlik Numarası giriniz.")]
        [StringLength(11,ErrorMessage ="Geçerli bir TC numarası giriniz")]
        [Required(ErrorMessage = "Bu alan zorunludur")]
        [Display(Name = "TC")]
        public string PatientTC { get; set; }

        [MinLength(4, ErrorMessage = "Minimum 4 karakter")]
        [MaxLength(15, ErrorMessage = "Maksimum 15 karakter")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bu alan zorunludur")]
        [Display(Name = "ŞİFRE")]
        public string PatientPassword { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

    }
}
