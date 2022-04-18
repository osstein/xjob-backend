using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;


/*Database model*/
public class News
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Titel")]
    public string? Title { get; set; }
    [Required]
    [Display(Name = "Innehåll")]
    public string? Content { get; set; }
    [Display(Name = "Sökväg bildfil")]
    public string? ImagePath { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    [Display(Name = "Bildbeskrivning")]
    public string? ImageAlt { get; set; }
    [Display(Name = "Tidsstämpel")]
    public DateTime Timestamp { get; set; } = DateTime.Now;
}