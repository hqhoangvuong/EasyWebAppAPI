using EasyWebApp.Data.Entities.AuthenticationEnties;
using EasyWebApp.Data.Entities.ServiceWebEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyWebApp.Data.DbContext
{
    public class EasyWebDbContext : IdentityDbContext<ApplicationUser>
    {
        public EasyWebDbContext(DbContextOptions<EasyWebDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserDbInfo> UserDatabaseInfos { get; set; }
    }
}
