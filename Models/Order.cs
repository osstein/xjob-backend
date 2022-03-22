using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class Order
{
    public int Id { get; set; }
    [Required]
    public string? CustomerFirstName { get; set; }
    [Required]
    public string? CustomerLastName { get; set; }
    [Required]
    [EmailAddress]
    public string? CustomerMail { get; set; }
    [Required]
    [Phone]
    public string? CustomerPhone { get; set; }
    [Required]
    public string? CustomerAdress { get; set; }
    [Required]
    public string? CustomerZip { get; set; }
    [Required]
    public string? CustomerCity { get; set; }

    [Required]
    public decimal? PriceTotal { get; set; }
    [Required]
    public decimal? VatTotal { get; set; }
    
    public decimal? DiscountTotal { get; set; } = 0;
    
    public string? DiscountCode { get; set; }

    [Required]
    public string? OrderNumber { get; set; }
    [Required]
    public string? ReceiptNumber { get; set; }

    [Required]
    public string? PaymentMethod { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.Now;

 
    public string? Status { get; set; } = "new";
    public List<OrderProducts>? OrderProducts { get; set; }
}