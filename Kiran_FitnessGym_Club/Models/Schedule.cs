using System.ComponentModel.DataAnnotations;

namespace Kiran_FitnessGym_Club.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            TrainerRegt = new HashSet<TrainerRegt>();
        }

        [Key]
        public int Id { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }

        public virtual ICollection<TrainerRegt> TrainerRegt { get; set; }
    }
}
