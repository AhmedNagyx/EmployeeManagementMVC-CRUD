using Microsoft.EntityFrameworkCore;

namespace MVCProject.Models
{
    public class ITIContext: DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }

        public ITIContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-6IC2DFK;Database=ITI; Encrypt=False; Trust Server Certificate=True; Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
