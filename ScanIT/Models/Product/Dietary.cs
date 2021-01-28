
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Dietary
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    [Display(Name = "Dietary")]
    public string  DietaryName { get; set; }

}
