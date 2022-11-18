using System.ComponentModel.DataAnnotations;

namespace PharmaEase.Models
{
    public class Courier
    {
        [Key]
        public int CourierId { get; set; }
        public string Name { get; set; }
        public int OperatingRange { get; set; }
    }
}
