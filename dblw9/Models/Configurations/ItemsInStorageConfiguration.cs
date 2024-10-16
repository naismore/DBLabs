using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dblw9.Models.Configurations
{
    public class ItemsInStorageConfiguration : IEntityTypeConfiguration<ItemsInStorage>
    {
        public void Configure(EntityTypeBuilder<ItemsInStorage> builder)
        {
            builder.Property(iis => iis.Id).HasColumnName("id");
            builder.HasKey(iis => iis.Id);
            builder.Property(iis => iis.StorageId).HasColumnName("id_storage");
            builder.Property(iis => iis.ItemId).HasColumnName("id_item");
            builder.Property(iis => iis.ArrialDate).HasColumnType("datetime").IsRequired();
        }    
    }
}
