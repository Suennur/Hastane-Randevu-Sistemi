namespace Hastane.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        
        [MinLength(2, ErrorMessage = "Minimum 2 karakter")]
        [MaxLength(50,ErrorMessage ="Maksimum 50 karakter")]
        [Required(ErrorMessage ="Bu alanın doldurulması zorunludur")]
        [Display(Name ="Kullanıcı Adı")]
        public string AdminName { get; set; }
        
        [MaxLength(20, ErrorMessage = "Maksimum 20 karakter")]
        [MinLength(4, ErrorMessage = "Minimum 4 karakter")]
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur")]
        [Display(Name = "Kullanıcı Şifre")]
        public string AdminPassword { get; set; }

    }
}
