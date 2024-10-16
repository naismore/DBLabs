using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace dblw9.Models.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(i => i.Id).HasColumnName("id");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(i => i.Unit)
                .HasColumnName("unit")
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(i => i.Cost)
                .HasColumnName("cost")
                .IsRequired();

            builder.Property(i=>i.Description)
                .HasColumnName("description")
                .HasDefaultValue("unspecified")
                .HasMaxLength(50);

            builder.Property(i => i.SupplierId)
                .HasColumnName("id_supplier")
                .IsRequired();
        }
    }
}
