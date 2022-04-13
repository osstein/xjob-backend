using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;


/*Database model*/
public class Contactform
{
    
    public int Id { get; set; }
    [NotMapped]
    public string? Name { get; set; }
    [NotMapped]
    public string? Email { get; set; }
    [NotMapped]
    public string? Message { get; set; }




}