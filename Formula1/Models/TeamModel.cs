using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class TeamModel
    {
        [Key]
        [Display(Name = "ID")]
        public int TeamId { get; set; }

        [Required]
        [Display(Name = "Team name")]
        public string Name { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }
    }
}