using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Models;

namespace ProductCatalog.Persistence.Configurations
{
    internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));

            builder.HasKey(product => product.Id);

            builder.Property(product => product.Name).IsRequired().HasMaxLength(100);

            builder.Property(product => product.Description).IsRequired().HasMaxLength(100);

            builder.Property(product => product.Price).HasPrecision(10,2);
        }
    }
}
