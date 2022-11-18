using System.ComponentModel.DataAnnotations;

namespace PharmaEase.Models
{
    public class Doctor
    {
        [Key]
        public int MedicalLicenseId { get; set; }
        public string Phone { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int ApprovAdminId { get; set; }
        public string FullName => Fname + " " + "Lname"; 
    }
}
