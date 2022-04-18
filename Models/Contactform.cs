using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;


/*Database model*/
public class Contactform
{
    
    public int Id { get; set; }
    [NotMapped]
    [Display(Name = "Namn")]
    public string? Name { get; set; }
    [NotMapped]
    [Display(Name = "E-post")]
    public string? Email { get; set; }
    [NotMapped]
    [Display(Name = "Meddelande")]
    public string? Message { get; set; }




}