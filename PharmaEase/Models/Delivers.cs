using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaEase.Models
{
    public class Delivers
    {
        [ForeignKey("Prescription")]
        public int PrescriptionID { get; set; }
        public Prescription Prescription { get; set; }

        [ForeignKey("Courier")]
        public int CourierID { get; set; }
        public Courier Courier { get; set; }
        [ForeignKey("Pharmacy")]
        public int PharmacyID { get; set; }
        public Pharmacy Pharmacy { get; set; }

        [ForeignKey("Patient")]
        public string PatientHealthNum { get; set; }
        public Patient Patient { get; set; }
    }
}
