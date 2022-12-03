using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaEase.Models
{
    public class Patient
    {
        [Key]
        public string GovtHealthNum { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string FullName => Fname + " " + Lname;
        public string ViewBag => FullName + " Health#: " + GovtHealthNum;
        [Required]
        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        public int DoctorId{ get; set; }
        public Doctor Doctor { get; set; }
    }
}
