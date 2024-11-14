
using System.ComponentModel.DataAnnotations.Schema;

namespace dblw9.Models
{
    [Table("item_in_storage")]
    public class ItemInStorage
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public Storage? Storage { get; set; }
        public int ItemId {  get; set; }
        public int Quantity { get; set; }
        public Item? Item { get; set; }
        public DateTime ArrialDate {  get; set; }
    } 
}