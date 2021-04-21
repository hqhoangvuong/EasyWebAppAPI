using EasyWebApp.Data.Entities.AuthenticationEnties;
using EasyWebApp.Data.Entities.SystemEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyWebApp.Data.DbContext
{
    public class CustomerDbContext : IdentityDbContext<ApplicationUser>
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SystemMasterConfig>()
                .HasQueryFilter(p => !p.IsDeleted)
                .HasIndex(p => new { p.ConfigName})
                .IsUnique();


            modelBuilder.Entity<SystemTableConfig>()
                .HasQueryFilter(p => !p.IsDeleted)
                .HasIndex(p => new { p.Name})
                .IsUnique();

            modelBuilder.Entity<SystemTableColumnConfig>()
                .HasQueryFilter(p => !p.IsDeleted)
                .HasIndex(p => new { p.TableId, p.Name})
                .IsUnique();

            modelBuilder.Entity<SystemTableForeingKeyConfig>().HasQueryFilter(p => !p.IsDeleted);
        }

        public DbSet<SystemMasterConfig> SystemMasterConfigs { get; set; }
        public DbSet<SystemTableConfig> SystemTableConfigs { get; set; }
        public DbSet<SystemTableColumnConfig> SystemTableColumnConfigs { get; set; }
        public DbSet<SystemTableForeingKeyConfig> SystemTableForeingKeyConfigs { get; set; }
    }
}
