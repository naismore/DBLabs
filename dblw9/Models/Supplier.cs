public class Supplier
{
    public int Id { get; set; }
    public string? Name { get; set; }    
    public string? ContactPerson {  get; set; }
    public string? ContactPersonFirstName { get; set; }
    public string? ContactPersonLastName { get; set; }
    public string? PhoneNumber {  get; set; }
    public string? EmailAddress { get; set;}
    public string? Adress {  get; set; }

    public List<Item> Items { get; set; } = new();
}