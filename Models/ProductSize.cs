using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class ProductSize
{
    public int Id { get; set; }
    [Required]

    public string? Size { get; set; }
  

    public List<ProductType>? ProductType { get; set; }
}