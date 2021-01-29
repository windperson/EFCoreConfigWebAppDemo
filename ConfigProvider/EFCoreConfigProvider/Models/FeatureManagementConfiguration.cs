using EFCoreConfigProvider.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreConfigProvider.Models
{
    public class FeatureManagementConfiguration : IEntityTypeConfiguration<FeatureManagement>
    {
        public void Configure(EntityTypeBuilder<FeatureManagement> builder)
        {
            builder.OwnsMany(e => e.Features);
        }
    }
}