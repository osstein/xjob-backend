using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class OrderProducts
{


    public int Id { get; set; }
    [Required]

    public int? ProductId { get; set; }
    [Required]

    public decimal? Price { get; set; }
    [Required]

    public int? Amount { get; set; }
    [Required]

    public string? ProductSize { get; set; }
    [Required]

    public string? ProductColor { get; set; }

    [Required]

    public string? ProductNumber { get; set; }
    
    public int? OrderId { get; set; }
    public Order? Order { get; set; }

}