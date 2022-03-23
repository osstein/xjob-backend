using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;


/*Database model*/
public class ProductImages
{
    public int Id { get; set; }
    
    public string? ImagePath { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    [Required]
    public string? ImageAlt { get; set; }

    [Required]
    public int? ProductId { get; set; }
    public Product? Product { get; set; }

}