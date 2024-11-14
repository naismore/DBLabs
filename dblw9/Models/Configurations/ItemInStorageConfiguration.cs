using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dblw9.Models.Configurations
{
    public class ItemInStorageConfiguration : IEntityTypeConfiguration<ItemInStorage>
    {
        public void Configure(EntityTypeBuilder<ItemInStorage> builder)
        {
            builder.Property(iis => iis.Id).HasColumnName("id");
            builder.HasKey(iis => iis.Id);
            builder.Property(iis => iis.StorageId).HasColumnName("id_storage");
            builder.Property(iis => iis.ItemId).HasColumnName("id_item");
            builder.Property(iis => iis.ArrialDate).HasColumnName("arrial_date").HasColumnType("datetime").IsRequired();
            builder.Property(iis => iis.Quantity).HasColumnName("quantity");
            
        }    
    }
}
