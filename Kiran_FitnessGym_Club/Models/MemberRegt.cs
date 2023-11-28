using Org.BouncyCastle.Asn1.Cmp;
using System.ComponentModel.DataAnnotations;

namespace Kiran_FitnessGym_Club.Models
{
    public partial class MemberRegt
    {
        public MemberRegt()
        {
            FeeDetails = new HashSet<FeeDetails>();
        }

        [Key]
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public long MobileNo { get; set; }
        public string City { get; set; }
        public DateTime? DateOfJoin { get; set; }
        public int? TrainerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual TrainerRegt Trainer { get; set; }
        public virtual ICollection<FeeDetails> FeeDetails { get; set; }
    }
}
