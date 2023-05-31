using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class CircuitModel
    {
        [Key]
        [Display(Name = "ID")]
        public int CircuitId { get; set; }

        [Required]
        // [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        // [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        // [Display(Name = "Country")]
        public string Country { get; set; }

        // // [Display(Name = "Latitude")]
        // [DisplayFormat(NullDisplayText = "Unknown")]
        // public string Latitude { get; set; }

        // // [Display(Name = "Longitude")]
        // [DisplayFormat(NullDisplayText = "Unknown")]
        // public string Longitude { get; set; }
    }
}