using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dblw9.Models
{
    [Table("customer")]
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        [MaxLength(50, ErrorMessage = "FirstName cannot exceed 50 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [MaxLength(50, ErrorMessage = "LastName cannot exceed 50 characters.")]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }


        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        public List<Order> Orders { get; set; } = new();
    }
}
