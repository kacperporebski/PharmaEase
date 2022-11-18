using System.ComponentModel.DataAnnotations;

namespace PharmaEase.Models
{
    public class Pharmacy
    {
        [Key]
        public int PharmacyId { get; set; }
        public string Address { get; set; }
        public string PhoneNum { get; set; }
    }
}
