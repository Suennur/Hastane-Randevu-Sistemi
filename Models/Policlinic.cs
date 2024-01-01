using System.ComponentModel.DataAnnotations;

namespace Hastane.Models
{
    public class Policlinic
    {
        [Key]
        public int PolicId{ get; set; }

        [Required]
        [Display(Name ="Policlinic")]
        public string? PolicName { get; set;}

    }
}
