using Micro.Services.IdentityAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Micro.Services.IdentityAPI.Data
{
    public class DBContext : IdentityDbContext<ApplicationUser>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) 
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }
    }
}
