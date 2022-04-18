using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class CatalogSubCategories
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Huvudkategori")]
    public string? Category { get; set; }
    [Required]
    [Display(Name = "Underkategori")]
    public int? CatalogCategoriesId { get; set; }
    public CatalogCategories? CatalogCategories { get; set; }

    public List<Product>? Product { get; set; }

}