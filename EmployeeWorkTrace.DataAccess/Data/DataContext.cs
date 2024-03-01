using EmployeeWorkTrace.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWorkTrace.DataAccess.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Works> Works { get; set; }
        public DbSet<Workers> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
