using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class Product
{


    public int Id { get; set; }
    [Required]

    public string? Name { get; set; }
    [Required]

    public decimal Price { get; set; }
    [Required]

    public decimal Vat { get; set; }
    [Required]

    public decimal Discount { get; set; }
    [Required]

    public string? Description { get; set; }
    [Required]

    public string? ProductNumber { get; set; }

    //Lägg till  bilder koppling
    public List<ProductImages>? ProductImages { get; set; }
    //Lägg till variantkoppling
    public List<ProductType>? ProductType { get; set; }

    public List<ProductProperties>? ProductProperties { get; set; }


    // Lägg till underkategori
     [Required]
    public int? CatalogSubCategoriesId { get; set; }
    public CatalogSubCategories? CatalogSubCategories { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.Now;


}