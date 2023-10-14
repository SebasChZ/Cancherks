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
        public DbSet<Request> Request { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<State> State { get; set; }

        public DbSet<InstallationSportViewModel> InstallationSportViewModels { get; set; } // spGetInstallationsbySport storeProcedure result
        public DbSet<InstallationAvailabilityViewModel> InstallationAvailabilityViewModels { get; set; } // storeProcedure result




        //public DbSet<Day> Day { get; set; }



        public DbSet<Sport> Sport { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InstallationSportViewModel>().HasNoKey().ToView(null);
            modelBuilder.Entity<InstallationAvailabilityViewModel>().HasNoKey().ToView(null);
        }
    }
}
