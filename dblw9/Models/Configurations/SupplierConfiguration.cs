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
                .HasColumnName("contact_person_first_name")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.ContactPersonLastName)
                .HasColumnName("contact_person_last_name")
                .IsRequired()
                .HasMaxLength(50);


            builder.Property(s => s.ContactPerson)
                .HasColumnName("contact_person")
                .HasComputedColumnSql("[contact_person_first_name] + ' ' + [contact_person_last_name]");
            
            builder.Property(s => s.PhoneNumber)
                .HasColumnName("phone_number")
                .IsRequired()
                .HasMaxLength(13);

            builder.Property(s => s.EmailAddress)
                .HasColumnName("email")
                .HasMaxLength(30);
            builder.Property(s => s.Adress)
                .HasColumnName("adress")
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}
