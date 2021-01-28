
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProductDietary
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Dietary")]
    public int DietaryId { get; set; }

    public Dietary Dietary { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }

    public Product Product { get; set; }
}
