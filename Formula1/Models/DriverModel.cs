using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class DriverModel
    {
        [Key]
        [Display(Name = "ID")]
        public int DriverId { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string Forename { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "3-letter alphabet code of driver")]
        public string Code { get; set; }

        [Display(Name = "Date of birth")]
        public string DateOfBirth { get; set; }

        [Display(Name = "Country")]
        public string Nationality { get; set; }
    }
}