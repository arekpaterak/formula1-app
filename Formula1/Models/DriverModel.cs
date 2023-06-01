using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class DriverModel
    {
        [Key]
        [Display(Name = "ID")]
        public int DriverId { get; set; }

        public string? Number { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string? Code { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string? LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(NullDisplayText = "Unknown")]
        [DataType(DataType.Date)]
        public string? DateOfBirth { get; set; }

        [Display(Name = "Nationality")]
        [DisplayFormat(NullDisplayText = "Unknown")]
        public string? Nationality { get; set; }

        [Display(Name = "Wikipedia Page")]
        [DisplayFormat(NullDisplayText = "Unknown")]
        public string? Url { get; set; }
    }
}