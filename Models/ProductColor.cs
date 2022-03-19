using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class ProductColor
{
    public int Id { get; set; }
    [Required]

    public string? Color { get; set; }
    
    [Required]

    public string? ColorCode { get; set; }
   

    public List<ProductType>? ProductType { get; set; }


}