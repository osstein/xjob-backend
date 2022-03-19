using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class CatalogCategories
{
    public int Id { get; set; }
    [Required]

    public string? Category { get; set; }
    
    public List<CatalogSubCategories>? CatalogSubCategories { get; set; }    

}