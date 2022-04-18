using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class ProductProperties
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Titel")]
    public string? Title { get; set; }
    [Required]
    [Display(Name = "Text")]
    public string? Text { get; set; }
    [Required]
    [Display(Name = "Produkt Id")]
    public int? ProductId { get; set; }
    public Product? Product { get; set; }
}