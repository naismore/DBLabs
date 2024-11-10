using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dblw9.Models.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("id");
            builder.Property(o => o.CustomerId).HasColumnName("id_customer");
            builder.Property(o => o.Adress).HasColumnName("adress");
            builder.Property(o => o.OrderDate).HasColumnName("order_date");
            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);


            // Начальные данные
            builder.HasData(
                new Order { Id = 1, CustomerId = 1, OrderDate = DateTime.Now, Adress = "Улица 1" },
                new Order { Id = 2, CustomerId = 2, OrderDate = DateTime.Now, Adress = "Улица 2" }
            );

        }
    }
}
