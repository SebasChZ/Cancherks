using CancherksWebApp.Model;
using Microsoft.EntityFrameworkCore;

namespace CancherksWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options) 
        {

        }
        public DbSet<Installation> Installation { get; set; }
    }
}
