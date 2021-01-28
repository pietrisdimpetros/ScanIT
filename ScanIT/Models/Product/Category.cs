

using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(400)]
    [Display(Name = "Category Name")]
    public string CategoryName { get; set; }

    [Required]
    public decimal VAT { get; set; }
}
