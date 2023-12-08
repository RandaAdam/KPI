using KPIAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext():base()
        {       
        }

        public ApplicationDbContext(DbContextOptions options)
        :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);
        }

        public DbSet<KPI> KPIs => Set<KPI>();
        public DbSet<Department> Departments => Set<Department>();
    }
}
