using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dblw9.Models.Configurations
{
    public class ItemsInOrderConfiguration : IEntityTypeConfiguration<ItemsInOrder>
    {
        public void Configure(EntityTypeBuilder<ItemsInOrder> builder)
        {
            builder.HasKey(io => io.Id);
            builder.Property(io => io.Id).HasColumnName("id");
            builder.Property(io => io.ItemId).HasColumnName("id_item");
            builder.Property(io => io.Quantity).HasColumnName("quantity");
            builder.Property(io => io.OrderId).HasColumnName("id_order");


            builder.HasOne(io => io.Item)
                .WithMany(i => i.ItemsInOrder)
                .HasForeignKey(io => io.ItemId);


            builder.HasOne(io => io.Order)
                .WithMany(o => o.ItemsInOrder)
                .HasForeignKey(io => io.ItemId);


            // Начальные данные

            builder.HasData(
                new ItemsInOrder { Id = 1, ItemId = 1, OrderId = 1, Quantity = 2 },
                new ItemsInOrder { Id = 2, ItemId = 2, OrderId = 1, Quantity = 1 },
                new ItemsInOrder { Id = 3, ItemId = 1, OrderId = 2, Quantity = 3 }
            );
        }
    }
}
