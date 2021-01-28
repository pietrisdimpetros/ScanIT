using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScanIT.ViewModels
{
    public class ProductVM
    {
        #region Product

        public int ProductId { get; set; }

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
        [Display(Name = "Selected Quantity")]
        public int SelectedQuantity { get; set; }

        public byte[] Photo { get; set; }

        [Required]
        [Range(0.0f, float.MaxValue, ErrorMessage = "Please enter a positive number")]
        public decimal InitialPrice { get; set; }

        //Images should be converted to base64 and stored as binary
        //barcode is essential to all prodcuts but qrcode not.
        public byte[] BarCode { get; set; }
        public byte[] QRCode { get; set; }

        public decimal CategoryVAT { get; set; }

       

        [Display(Name = "Price")]

        public decimal PriceIncludingVAT
        {
            get { return (CategoryVAT + 1.0m) * InitialPrice; }
        }

        #endregion

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }


        public int[] DietariesIds { get; set; }
        public IEnumerable<Dietary> Dietaries { get; set; }
    }
}