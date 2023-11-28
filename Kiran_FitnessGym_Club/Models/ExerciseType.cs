using System.ComponentModel.DataAnnotations;

namespace Kiran_FitnessGym_Club.Models
{
    public class ExerciseType
    {
        [Key]
        public int ExercciseId { get; set; }
        [Required]      

        public string ExerciseName { get; set; }

    }
}
