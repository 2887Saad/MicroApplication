using Micro.Services.CompanyAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace Micro.Services.CompanyAPI.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext>options) : base(options)
        {

        }
        public DbSet<Company> Companies { get; set; }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }
    }
}
