using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task2Application.Entity;

public class ProductConfiguration
    : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.ProductName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.UnitPrice)
            .HasPrecision(18, 2);

        // Composite Index
        builder.HasIndex(p => new
        {
            p.CategoryId,
            p.ProductName
        });

        // Relationship
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        // Soft Delete
        builder.Property(p => p.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasQueryFilter(p => !p.IsDeleted);

        // Optimistic Concurrency
        builder.Property(p => p.RowVersion)
            .IsRowVersion();
    }
}