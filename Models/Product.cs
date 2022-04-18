using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class Product
{


    public int Id { get; set; }
    [Required]
    [Display(Name = "Namn")]
    public string? Name { get; set; }
    [Required]
    [Display(Name = "Pris")]
    public decimal Price { get; set; }
    [Required]
    [Display(Name = "Moms(%)")]
    public decimal Vat { get; set; }
    [Required]
    [Display(Name = "Rabatt(%)")]
    public decimal Discount { get; set; }
    [Required]
    [Display(Name = "Beskrivning")]
    public string? Description { get; set; }
    [Required]
    [Display(Name = "Artikelnummer")]
    public string? ProductNumber { get; set; }


    public List<ProductImages>? ProductImages { get; set; }

    public List<ProductType>? ProductType { get; set; }

    public List<ProductProperties>? ProductProperties { get; set; }



    [Required]
    [Display(Name = "Kategori")]
    public int? CatalogSubCategoriesId { get; set; }
    public CatalogSubCategories? CatalogSubCategories { get; set; }
    [Display(Name = "Tidsstämpel")]
    public DateTime Timestamp { get; set; } = DateTime.Now;


}