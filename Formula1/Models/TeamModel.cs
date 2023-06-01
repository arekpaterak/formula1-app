using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class TeamModel
    {
        [Key]
        [Display(Name = "ID")]
        public int TeamId { get; set; }

        [Required]
        [Display(Name = "Team Name")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        public string? Nationality { get; set; }

        [Display(Name = "Wikipedia Page")]
        [DisplayFormat(NullDisplayText = "Unknown")]
        public string? Url { get; set; }
    }
}