using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class ProductType
{
    public int Id { get; set; }
    [Required]

    public int? Amount { get; set; }
    [Required]
    
    public int? ProductId { get; set; }
    public Product? Product { get; set; }
    [Required]
    public int? ProductColorId { get; set; }
    public ProductColor? ProductColor { get; set; }
    [Required]
    public int? ProductSizeId { get; set; }
    public ProductSize? ProductSize { get; set; }

}