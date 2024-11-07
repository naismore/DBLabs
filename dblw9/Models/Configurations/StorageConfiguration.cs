using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dblw9.Models.Configurations
{
    public class StorageConfiguration : IEntityTypeConfiguration<Storage>
    {
        public void Configure(EntityTypeBuilder<Storage> builder)
        {
            builder.Property(s => s.Id).HasColumnName("id");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(s => s.Adress)
                .HasColumnName("adress")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.PhoneNumber)
                .HasColumnName("phone_number")
                .IsRequired()
                .HasMaxLength(13);
            
        }
    }
}
