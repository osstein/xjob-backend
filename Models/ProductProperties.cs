using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class ProductProperties
{
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? Text { get; set; }
    [Required]
    public int? ProductId { get; set; }
    public Product? Product { get; set; }
}