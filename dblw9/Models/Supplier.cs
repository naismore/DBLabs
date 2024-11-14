using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dblw9.Models
{
    [Table("supplier")]
    public class Supplier
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Contact person first name is required.")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string? ContactPersonFirstName { get; set; }

        public string? ContactPerson { get; set; }

        [Required(ErrorMessage = "Contact person last name is required.")]
        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string? ContactPersonLastName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(13, ErrorMessage = "Phone number cannot exceed 13 characters.")]
        public string? PhoneNumber { get; set; }

        [MaxLength(30, ErrorMessage = "Email address cannot exceed 30 characters.")]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(50, ErrorMessage = "Address cannot exceed 50 characters.")]
        public string? Adress { get; set; }

        public List<Item> Items { get; set; } = new();
    }

}