using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaEase.Models
{
    public class Delivers
    {
        [Key, Column(Order = 1)]
        public DateTime TimeDelivered { get; set; }

        [Key, ForeignKey("Prescription"), Column(Order = 2)]
        public int PrescriptionID { get; set; }
        public Prescription Prescription { get; set; }

        [ForeignKey("Courier"), Column(Order = 3)]
        public int CourierID { get; set; }
        public Courier Courier { get; set; }

        [ForeignKey("Pharmacy"), Column(Order = 4)]
        public int PharmacyID { get; set; }
        public Pharmacy Pharmacy { get; set; }

        [ForeignKey("Patient"), Column(Order =5 )]
        public string PatientHealthNum { get; set; }
        public Patient Patient { get; set; }

    }
}
