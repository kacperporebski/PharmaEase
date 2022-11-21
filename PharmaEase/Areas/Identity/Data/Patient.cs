using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PharmaEase.Areas.Identity.Data
{
    public class Patient : IdentityUser
    {
        public string GovtHealthNum { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string FullName => Fname + " " + Lname;
    }
}
