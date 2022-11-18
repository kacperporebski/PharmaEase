using System.ComponentModel.DataAnnotations;

namespace PharmaEase.Models
{
    public class Patient
    {
        [Key]
        public int GovtHealthNum { get; set; }
        public string PhoneNum { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int BuildingNum { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string FullName => Fname + " " + Lname;
    }
}
