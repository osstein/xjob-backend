using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class DiscountCodes
{
    public int Id { get; set; }
    [Required]
[Display(Name = "Rabattkod")]
    public string? Code { get; set; }
    [Required]
    [Display(Name = "Rabatt(%)")]
    public decimal Discount { get; set; }
    [Required]
    [Display(Name = "Startdatum")]
    public DateTime CampaignStart { get; set; }
    [Required]
    [Display(Name = "Slutdatum")]
    public DateTime CampaignEnd { get; set; }
    [Display(Name = "Tidsstämpel")]
    public DateTime Timestamp { get; set; } = DateTime.Now;

}