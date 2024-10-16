using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dblw9.Models.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.Property(s => s.Id).HasColumnName("id");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.ContactPersonFirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.ContactPersonLastName)
                .IsRequired()
                .HasMaxLength(50);


            builder.Property(s => s.ContactPerson)
                .HasComputedColumnSql("[ContactPersonFirstName] + ' ' + [ContactPersonLastName]");
            
            builder.Property(s => s.PhoneNumber)
                .IsRequired()
                .HasMaxLength(13);

            builder.Property(s => s.EmailAddress).HasMaxLength(30);
            builder.Property(s => s.Adress)
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}
