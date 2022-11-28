using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaEase.Models
{
    public class Pharmacist
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }

        public string ApprovAdminID { get; set; }

        [Required]
        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [Required]
        [ForeignKey("Pharmacy")]
        public int WorksAt { get; set; }
        public Pharmacy Pharmacy { get; set; }
    }
}
