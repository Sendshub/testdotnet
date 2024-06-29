using Microsoft.EntityFrameworkCore;

namespace TestDotNet.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<DataItem> DataItems { get; set; }
    }
}