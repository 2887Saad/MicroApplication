using Micro.Services.CategoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Micro.Services.CategoryAPI.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }
    }
}
