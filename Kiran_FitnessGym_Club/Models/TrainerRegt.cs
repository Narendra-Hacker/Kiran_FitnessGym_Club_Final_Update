using System.ComponentModel.DataAnnotations;


namespace Kiran_FitnessGym_Club.Models
{
    public partial class TrainerRegt
    {
        public TrainerRegt()
        {
            MemberRegt = new HashSet<MemberRegt>();
        }

        [Key]

        public int TrainerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long MobileNo { get; set; }
        public int? Experience { get; set; }
        public int? Salary { get; set; }
        public int? TrainingFees { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? ScheduleId { get; set; }

        public virtual Schedule Schedule { get; set; }
        public virtual ICollection<MemberRegt> MemberRegt { get; set; }
    }
}
