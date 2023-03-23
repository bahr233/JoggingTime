using Microsoft.EntityFrameworkCore;

namespace JoggingTime.Models
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserJoggingTime> UserJoggingTimes { get; set; }
        public DbSet<Token> Tokens { get; set; }
    }
}
