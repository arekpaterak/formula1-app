using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class RaceResultsModel
    {
        [Key]
        [Display(Name = "ID")]
        public int ResultId { get; set; }

        [Required]
        public RaceModel? Race { get; set; }

        [Required]
        public DriverModel? Driver { get; set; }

        [Required]
        public TeamModel? Team { get; set; }

        [Required]
        [Display(Name = "Position")]
        public string? Position { get; set; }

        [Required]
        [Display(Name = "Starting Grid")]
        public int StartingGrid { get; set; }

        [Required]
        [Display(Name = "Laps")]
        public int Laps { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Time/Retired")]
        public string? FastestLapTime { get; set; }

        [Required]
        [Display(Name = "Points")]
        public int Points { get; set; }

        [Required]
        [Display(Name = "Set Fastest Lap?")]
        public string? SetFastestLap { get; set; }

        [Required]
        [Display(Name = "Fastest Lap Time")]
        public string? FastestLap { get; set; }
    }
}