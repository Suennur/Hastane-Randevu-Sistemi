using System.ComponentModel.DataAnnotations;
namespace Hastane.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        
        [MinLength(2, ErrorMessage = "Minimum 2 karakter")]
        [MaxLength(25, ErrorMessage = "Maksimum 25 karakter")]
        [Required(ErrorMessage ="Bu alanın doldurulması zorunludur.")]
        [Display(Name = "AD")]
        public string DoctorFName { get; set;}

        
        [MinLength(2, ErrorMessage = "Minimum 2 karakter")]
        [MaxLength(20, ErrorMessage = "Maksimum 20 karakter")]
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        [Display(Name = "SOYAD")]
        public string DoctorLName { get; set;}


        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        [Display(Name = "ÇALIŞMA GÜNLERİ")]
        public DayOfWeek WorkDay { get; set;}

        [ForeignKey("PolicID")]
        Policlinic? policlinic { get; set; }
        public int PolicID { get; set; }

        
        public ICollection<Appointment> Appointments { get; set; }

    }
}
