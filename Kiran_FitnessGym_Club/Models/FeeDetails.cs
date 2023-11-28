using System.ComponentModel.DataAnnotations;

namespace Kiran_FitnessGym_Club.Models
{
    public class FeeDetails
    {
        [Key]
        public int FeeId { get; set; }
        public int? MemberId { get; set; }
        public int? Subscription { get; set; }
        public int? TotalFees { get; set; }
        public int? AmountPaid { get; set; }
        public int? FeeDue { get; set; }
        public string Status { get; set; }

        public virtual MemberRegt Member { get; set; }


    
    }
}
