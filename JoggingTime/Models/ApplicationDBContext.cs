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
        public DbSet<User> users { get; set; }
        public DbSet<UserJoggingTime> userJoggingTimes { get; set; }
    }
}
