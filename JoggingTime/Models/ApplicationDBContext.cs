using Microsoft.EntityFrameworkCore;

namespace JoggingTime.Models
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {

        }
    }
}
