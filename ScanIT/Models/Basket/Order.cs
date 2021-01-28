
using ScanIT.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("PaymentStatus")]
    public int PaymentStatusId { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    [ForeignKey("OrderStatus")]
    public int OrderStatusId { get; set; }

    public OrderStatus OrderStatus { get; set; }


    [ForeignKey("PaymentMethod")]
    public int PaymentMethodId { get; set; }

    public PaymentMethod PaymentMethod { get; set; }


    [ForeignKey("ApplicationUser")]
    public string ApplicationUserId { get; set; }

    public ApplicationUser ApplicationUser { get; set; }

    
    public DateTime WhenCreated { get; }  

    public Order()
    {
        this.WhenCreated = DateTime.Now;
        this.OrderStatusId = 1;
        this.PaymentStatusId = 1;
        this.PaymentMethodId = 1;
    }

  
}
