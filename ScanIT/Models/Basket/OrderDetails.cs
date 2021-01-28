
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderDetails
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }

    public Product Product { get; set; }

    public decimal Price { get; set; }

    
    public int SelectedQuantity { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    public Order Order { get; set; }

    //These fields will be used in further version

    #region FurtherVersions

    public float Discount { get; set; }

    public decimal DiscountAmount { get; set; }

    public string Metric { get; set; }

    #endregion


}
