using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using EFCoreConfigProvider.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConfigProvider.Models
{
    public class FeatureEntityDbContext : DbContext
    {
        public FeatureEntityDbContext([NotNull] DbContextOptions<FeatureEntityDbContext> options) : base(options)
        {
        }

        public DbSet<FeatureManagement> FeatureManagements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new FeatureManagementConfiguration());
            var outsideConfigAssembly = Assembly.GetEntryAssembly();
            if (outsideConfigAssembly != null)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(outsideConfigAssembly);
            }
        }

        public override int SaveChanges()
        {
            OnEntityChange();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnEntityChange();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnEntityChange()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(i => i.State == EntityState.Modified || i.State == EntityState.Added))
            {
                EntityChangeObserver.Instance.OnChanged(new EntityChangeEventArgs(entry));
            }
        }
    }
}