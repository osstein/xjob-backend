using System.ComponentModel.DataAnnotations;

namespace backend.Models;


/*Database model*/
public class DiscountCodes
{
    public int Id { get; set; }
    [Required]

    public string? Code { get; set; }
    [Required]
    public decimal Discount { get; set; }
    [Required]
    public DateTime CampaignStart { get; set; }
    [Required]
    public DateTime CampaignEnd { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;

}