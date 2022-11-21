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

        //this needs to come from the the asp net user table
        public string ApprovAdminId { get; set; }  
        public string FullName => Fname + " " + "Lname"; 
    }
}
