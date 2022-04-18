using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;


/*Database model*/
public class Episode
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Titel")]
    public string? Title { get; set; }
    [Required]
    [Display(Name = "Beskrivning")]
    public string? Description { get; set; }

    [Required]
    [Display(Name = "Säsong")]
    public string? S { get; set; }
    [Required]
    [Display(Name = "Avsnitt")]
    public string? E { get; set; }
    [Display(Name = "Filens sökväg")]
    
    public string? FilePath { get; set; }
    [NotMapped]
    public IFormFile? File { get; set; }
    [Display(Name = "Tidsstämpel")]
    public DateTime Timestamp { get; set; } = DateTime.Now;
}