using Microsoft.EntityFrameworkCore;

namespace MainDbAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Countries> Country{ get; set; }
    }
}
