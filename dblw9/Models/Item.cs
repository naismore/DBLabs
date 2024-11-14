using dblw9.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dblw9.Models
{
    [Table("item")]
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [MaxLength(50, ErrorMessage = "Description cannot exceed 50 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Cost is required.")]
        public double Cost { get; set; }

        [Required(ErrorMessage = "Unit is required.")]
        [MaxLength(10, ErrorMessage = "Unit cannot exceed 10 characters.")]
        public string? Unit { get; set; }
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public List<ItemInStorage> ItemInStorage { get; set; } = new();
        public List<ItemInOrder> ItemInOrder { get; set; } = new();
    }
}


