using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;


/*Database model*/
public class OrderProducts
{


    public int Id { get; set; }
    [Required]
    [Display(Name = "Artikel")]
    public int? ProductId { get; set; }
    [Required]
    [Display(Name = "Pris")]
    public decimal Price { get; set; }
    [Required]
    [Display(Name = "Antal")]
    public int Amount { get; set; }
    [Display(Name = "Produkttyp")]
    [NotMapped]
    public int TypeId { get; set; }

    [Required]
    [Display(Name = "Storlek")]
    public string? ProductSize { get; set; }
    [Required]
    [Display(Name = "Färg")]
    public string? ProductColor { get; set; }

    [Required]
    [Display(Name = "Artikelnummer")]
    public string? ProductNumber { get; set; }
    [Display(Name = "Order")]
    public int? OrderId { get; set; }
    public Order? Order { get; set; }

}