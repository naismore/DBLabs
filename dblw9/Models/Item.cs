public class Item
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Cost { get; set; }
    public string? Unit { get; set; }
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public List<ItemsInStorage> ItemsInStorages { get; set; } = new();
}

