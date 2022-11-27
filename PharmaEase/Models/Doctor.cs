using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string FullNameAndNumber => FullName + " - Phone Number: " + Phone;
        public string FullName => Fname + " " + Lname;


        [ForeignKey("IdentityUserKey")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
