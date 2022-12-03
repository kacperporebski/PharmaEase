using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaEase.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int Refills { get; set; }
        public int Dosage { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Doctor")]
        public int PrescriberLicenseNum { get; set; }
        public Doctor Doctor { get; set; }

        [DisplayName("Patient")]
        [ForeignKey("Patient")]
        public string PatientHealthNum { get; set; }
        public Patient Patient { get; set; }


        [Required]
        [ForeignKey("Medication")]
        public string MedicationCommonName { get; set; }
        public Medication Medication { get; set; }
        public int decrementRefill { get { return Refills- 1; } }
    }
}
