using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using PharmaEase.Data;
using PharmaEase.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace PharmaEase.Views.Pharmacists
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CreatePharmacistModel
    {

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Medical License Number")]
        public int MedLicenseNum { get; set; }

        [Required]
        [Display(Name = "Address of Employer")]
        public int WorksAt { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

}
