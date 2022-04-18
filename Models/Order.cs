using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class Order
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Förnamn")]
    public string? CustomerFirstName { get; set; }
    [Required]
    [Display(Name = "Efternamn")]
    public string? CustomerLastName { get; set; }
    [Required]
    [EmailAddress]
    [Display(Name = "Mail")]
    public string? CustomerMail { get; set; }
    [Required]
    [Phone]
    [Display(Name = "Telefonnummer")]
    public string? CustomerPhone { get; set; }
    [Required]
    [Display(Name = "Address")]
    public string? CustomerAdress { get; set; }
    [Required]
    [Display(Name = "Postkod")]
    public string? CustomerZip { get; set; }
    [Required]
    [Display(Name = "Stad")]
    public string? CustomerCity { get; set; }

    [Display(Name = "Summa pris")]
    public decimal? PriceTotal { get; set; } = 0;
    [Display(Name = "Summa Moms")]
    public decimal? VatTotal { get; set; } = 0;
    [Display(Name = "Summa rabatt")]
    public decimal? DiscountTotal { get; set; } = 0;
    [Display(Name = "Rabattkod")]
    public string? DiscountCode { get; set; } = "";

    [Display(Name = "Ordernummer")]
    public string? OrderNumber { get; set; } = "";
    [Display(Name = "Kvittonummer")]
    public string? ReceiptNumber { get; set; } = "";

    [Required]
    [Display(Name = "Betalmetod")]
    public string? PaymentMethod { get; set; }
    [Display(Name = "Tidsstämpel")]
    public DateTime Timestamp { get; set; } = DateTime.Now;

    [Display(Name = "Status")]
    public string? Status { get; set; } = "New";
    public List<OrderProducts>? OrderProducts { get; set; }
}