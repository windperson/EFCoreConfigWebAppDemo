using EFCoreConfigProvider.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreConfigWebAppDemo.EntityConfigurations
{
    public class FeatureManagementConfiguration: IEntityTypeConfiguration<FeatureManagement>
    {
        public void Configure(EntityTypeBuilder<FeatureManagement> builder)
        {
            builder.ToContainer("GalleryFeatureFlags");
        }
    }
}