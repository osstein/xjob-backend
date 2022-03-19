

namespace backend.Models;


/*Database model*/
public class OrderPostRequest
{
    public Order? Order { get; set; }

     public List<OrderProducts>? OrderProducts { get; set; }
    
}

