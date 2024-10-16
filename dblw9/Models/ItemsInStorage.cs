public class ItemsInStorage
{
    public int Id { get; set; }
    public int StorageId { get; set; }
    public Storage? Storage { get; set; }
    public int ItemId {  get; set; }
    public Item? Item { get; set; }
    public DateTime ArrialDate {  get; set; }
} 