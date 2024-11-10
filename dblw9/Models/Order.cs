using System.ComponentModel.DataAnnotations;

namespace dblw9.Models
{
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
        public List<ItemsInOrder> ItemsInOrder { get; set; } = new();
    }
}
