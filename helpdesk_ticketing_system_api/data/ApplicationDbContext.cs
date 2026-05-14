using Microsoft.EntityFrameworkCore;

namespace helpdesk_ticketing_system_api.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        
    }
}
