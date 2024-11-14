using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dblw9.Models.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.FirstName).HasColumnName("first_name");
            builder.Property(c => c.LastName).HasColumnName("last_name");
            builder.Property(c => c.BirthDate).HasColumnName("birth_date");
            builder.Property(c => c.Email).HasColumnName("email");

        }
    }
}
