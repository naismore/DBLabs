using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dblw9.Models
{
    [Table("order")]
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string? Adress { get; set; }

        public virtual Customer? Customer { get; set; }
        public List<ItemInOrder> ItemInOrder { get; set; } = new();
    }
}
