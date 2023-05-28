using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class RaceModel
    {
        [Key]
        [Display(Name = "ID")]
        public int RaceId { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Round { get; set; }

        public CircuitModel? Circuit { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public string Date { get; set; }
    }
}