using Microsoft.EntityFrameworkCore;

namespace API.DbContexts
{
    public class InterviewDBContext: DbContext
    {
        public InterviewDBContext(DbContextOptions<InterviewDBContext> options) : base(options) { }

        public virtual DbSet<Entities.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.User>(entity =>
            {
                entity.ToTable("users");
            });
        }
    }
}
