
using System.ComponentModel.DataAnnotations;

public class PaymentStatus
{
    [Key]
    public int Id { get; set; }


    // Paid/Pending
    public string PaymentStatusName { get; set; }

}
