using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class ProductType
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Antal")]
    public int? Amount { get; set; }
    [Required]
    [Display(Name = "Produkt Id")]
    public int? ProductId { get; set; }
    [Display(Name = "Produkt")]
    public Product? Product { get; set; }
    [Required]
    [Display(Name = "Produktfärg")]
    public int? ProductColorId { get; set; }
    [Display(Name = "Produktfärg")]
    public ProductColor? ProductColor { get; set; }
    [Required]
    [Display(Name = "Produktstorlek")]
    public int? ProductSizeId { get; set; }
    [Display(Name = "Produktstorlek")]
    public ProductSize? ProductSize { get; set; }

}