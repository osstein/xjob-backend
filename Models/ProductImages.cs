using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;


/*Database model*/
public class ProductImages
{
    public int Id { get; set; }
   [Display(Name = "Bildens sökväg")]
    public string? ImagePath { get; set; }

    [NotMapped]
    
    public IFormFile? ImageFile { get; set; }

    [Required]
    [Display(Name = "Bild beskrivning")]
    public string? ImageAlt { get; set; }

    [Required]
    [Display(Name = "Produkt Id")]
    public int? ProductId { get; set; }
    public Product? Product { get; set; }

}