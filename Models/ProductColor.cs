using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class ProductColor
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Färg")]
    public string? Color { get; set; }

    [Required]
    [Display(Name = "Färgkod")]
    public string? ColorCode { get; set; }


    public List<ProductType>? ProductType { get; set; }


}