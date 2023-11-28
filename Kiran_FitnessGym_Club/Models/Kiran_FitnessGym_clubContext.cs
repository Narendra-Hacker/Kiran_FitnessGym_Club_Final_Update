using Microsoft.EntityFrameworkCore;
using Kiran_FitnessGym_Club.Models;

namespace Kiran_FitnessGym_Club.Models
{
    public class Kiran_FitnessGym_clubContext : DbContext
    {
        public Kiran_FitnessGym_clubContext(DbContextOptions<Kiran_FitnessGym_clubContext> options) : base(options)
        {

        }

        public DbSet<TrainerRegt> TrainerRegt { get; set; }
        public DbSet<MemberRegt> MemberRegt { get; set; }

        public DbSet<FeeDetails> FeeDetails { get; set; }

        public DbSet<Schedule> Schedule { get; set; }

        public DbSet<Kiran_FitnessGym_Club.Models.ExerciseType> ExerciseType { get; set; }

        public DbSet<Kiran_FitnessGym_Club.Models.Feedback> Feedback { get; set; }


    }
}
