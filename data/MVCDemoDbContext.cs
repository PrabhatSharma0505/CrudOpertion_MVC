using DEMO.Models.DomainModel;
using Microsoft.EntityFrameworkCore;
namespace DEMO.data
{
    public class MVCDemoDbContext:DbContext
    {
        internal object employee;

        public MVCDemoDbContext (DbContextOptions<MVCDemoDbContext> options):base(options)
        {
        }

        public DbSet <Employee> Employees { get; set; }
    }
}
