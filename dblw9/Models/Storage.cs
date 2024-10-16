using System.Net;

public class Storage
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Adress { get; set; }
    public string? PhoneNumber { get; set; }
    public List<ItemsInStorage> ItemsInStorages { get; set; } = new();
}