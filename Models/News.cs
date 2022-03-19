using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;


/*Database model*/
public class News
{
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? Content { get; set; }
    
    public string? ImagePath { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public string? ImageAlt { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.Now;
}