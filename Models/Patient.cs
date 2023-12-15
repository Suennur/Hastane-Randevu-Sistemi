namespace Hastane.Models
{
    public class Patient
    {
        public int PatientID { get; set; }

        public string PatientFName { get; set; } = string.Empty;

        public string PatientLName { get; set; }

        public string PatientTC { get; set; }

        public string PatientPassword { get; set; }
    }
}
