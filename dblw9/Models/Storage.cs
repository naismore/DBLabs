using System.ComponentModel.DataAnnotations;

public class Storage
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [MaxLength(50, ErrorMessage = "Address cannot exceed 50 characters.")]
    public string? Adress { get; set; }

    [Required(ErrorMessage = "PhoneNumber is required.")]
    [MaxLength(13, ErrorMessage = "PhoneNumber cannot exceed 13 characters.")]
    public string? PhoneNumber { get; set; }

    public List<ItemsInStorage> ItemsInStorages { get; set; } = new();
}