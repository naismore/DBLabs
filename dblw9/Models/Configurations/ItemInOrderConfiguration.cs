using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dblw9.Models.Configurations
{
    public class ItemInOrderConfiguration : IEntityTypeConfiguration<ItemInOrder>
    {
        public void Configure(EntityTypeBuilder<ItemInOrder> builder)
        {
            builder.HasKey(io => io.Id);
            builder.Property(io => io.Id).HasColumnName("id");
            builder.Property(io => io.ItemId).HasColumnName("id_item");
            builder.Property(io => io.Quantity).HasColumnName("quantity");
            builder.Property(io => io.OrderId).HasColumnName("id_order");


            builder.HasOne(io => io.Item)
                .WithMany(i => i.ItemInOrder)
                .HasForeignKey(io => io.ItemId);


            builder.HasOne(io => io.Order)
                .WithMany(o => o.ItemInOrder)
                .HasForeignKey(io => io.ItemId);

        }
    }
}
