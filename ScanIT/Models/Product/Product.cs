


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    [Display(Name ="Product")]
    public string ProductName { get; set; }
        
    [Required]
    [MaxLength(400)]
    public string Description { get; set; }

    //Available Quantity will be further used in next versions of this application.
    //The company will be required to perform an everyday stock count in order for this property to be updated.
    [Required]
    [Range(0, Int32.MaxValue, ErrorMessage = "Please enter a positive integer number")]
    [Display(Name = "Available Quantity")]
    public int AvailableQuantity { get; set; }

    [Required]
    public byte[] Photo { get; set; }

    [Required]
    [Range(0.0f, float.MaxValue, ErrorMessage = "Please enter a positive number")]
    public decimal InitialPrice { get; set; }

    //Images should be converted to base64 and stored as binary
    //barcode is essential to all prodcuts but qrcode not.
    [Required] 
    public byte[] BarCode { get; set; }
    public byte[] QRCode { get; set; }

    public bool IsInactive { get; set; }

    //Category's Foreign Key

    [ForeignKey("Category")]
    public int CategoryId { get; set; }

    public Category Category { get; set; }


    [Display(Name = "Price")]
    public decimal PriceIncludingVAT
    {
        get { return (Category.VAT + 1.0m) * InitialPrice; }
        
        
    }



}
