using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class SprintResultsModel
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
        int Grid { get; set; }

        string Position { get; set; }

        int PositionOrder { get; set; }

        int Points { get; set; }
    }
}