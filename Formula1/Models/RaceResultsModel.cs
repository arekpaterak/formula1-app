using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class RaceResultsModel
    {
        [Key]
        [Display(Name = "ID")]
        public int ResultId { get; set; }

        [Required]
        public RaceModel? RaceId { get; set; }

        [Required]
        public DriverModel? DriverId { get; set; }

        [Required]
        public TeamModel? TeamId { get; set; }

        [Required]
        [Display(Name = "Car Number")]
        public int Number { get; set; }

        [Required]
        public int Grid { get; set; }

        // [Display(Name = "Position")]
        // public string Position { get; set; }

        [Display(Name = "Position")]
        public int PositionOrder { get; set; }

        [Display(Name = "Points")]
        public int Points { get; set; }
    }
}