using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class CatalogCategories
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Huvudkategori")]
    public string? Category { get; set; }

    public List<CatalogSubCategories>? CatalogSubCategories { get; set; }

}