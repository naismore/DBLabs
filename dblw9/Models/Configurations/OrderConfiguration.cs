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

        }
    }
}
