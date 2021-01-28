
using System.ComponentModel.DataAnnotations;

public class OrderStatus
{
    [Key]
    public int Id { get; set; }

    public string OrderStatusName { get; set; }


    //Ongoing - 1
    //Completed - 2
    //Dispatched - 3
}
