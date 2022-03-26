using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;


/*Database model*/
public class Episode
{
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? Description { get; set; }

    [Required]
    public string? S { get; set; }
    [Required]
    public string? E { get; set; }
    
    public string? FilePath { get; set; }
    [NotMapped]
    public IFormFile? File { get; set; }
    
    public DateTime Timestamp { get; set; } = DateTime.Now;
}