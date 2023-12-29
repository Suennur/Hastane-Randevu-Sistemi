using System.ComponentModel.DataAnnotations;
namespace Hastane.Models
{
    public class Policlinic
    {
        [Key]
        public int PolicID { get; set; }

        public string PolicName { get; set; }
    }
} 
