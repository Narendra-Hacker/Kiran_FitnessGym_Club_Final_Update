// Feedback.cs

using System;
using System.ComponentModel.DataAnnotations;

namespace Kiran_FitnessGym_Club.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        [Required]
        public int TrainerId { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        [MaxLength(500)]
        public string FeedbackText { get; set; }

        // New property for ratings
        [Range(1, 5)] // Assuming ratings are on a scale from 1 to 5
        public int Rating { get; set; }

        // Navigation properties
        public virtual TrainerRegt Trainer { get; set; }
        public virtual MemberRegt Member { get; set; }
    }
}
