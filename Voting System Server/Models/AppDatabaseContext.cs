using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Voting_System_Server.Models
{
    public partial class AppDatabaseContext : DbContext
    {
        private static AppDatabaseContext db = null;

        public static AppDatabaseContext GetInstance()
        {
            if (db == null)
                db = new AppDatabaseContext();
            return db;
        }

        private AppDatabaseContext()
            : base("name=AppDatabaseContext")
        {
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Vote> Votes { get; set; }
        public virtual DbSet<VoteEvent> VoteEvents { get; set; }
        public virtual DbSet<VoteTurn> VoteTurns { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .HasMany(e => e.Votes)
                .WithRequired(e => e.AppUser)
                .HasForeignKey(e => e.ID_USER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AppUser>()
                .HasMany(e => e.VoteEvents)
                .WithRequired(e => e.AppUser)
                .HasForeignKey(e => e.ID_HOST)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Candidate>()
                .HasMany(e => e.VoteTurns)
                .WithRequired(e => e.Candidate)
                .HasForeignKey(e => e.ID_CANDIDATE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vote>()
                .HasMany(e => e.VoteTurns)
                .WithRequired(e => e.Vote)
                .HasForeignKey(e => e.ID_VOTE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VoteEvent>()
                .HasMany(e => e.Candidates)
                .WithRequired(e => e.VoteEvent)
                .HasForeignKey(e => e.ID_VOTE_EVENT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VoteEvent>()
                .HasMany(e => e.Votes)
                .WithRequired(e => e.VoteEvent)
                .HasForeignKey(e => e.ID_VOTE_EVENT)
                .WillCascadeOnDelete(false);
        }
    }
}
