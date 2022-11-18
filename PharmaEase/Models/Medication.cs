using System.ComponentModel.DataAnnotations;

namespace PharmaEase.Models
{
    public class Medication
    {
        [Key]
        public string CommonName { get; set; }    
        public int Miligrams { get; set; }
        public int DoseNum { get; set; }
    }
}
