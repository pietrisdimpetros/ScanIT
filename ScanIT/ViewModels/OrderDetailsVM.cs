using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScanIT.ViewModels
{
    public class OrderDetailsVM
    {
        [Display(Name = "Product")]
        public string ProductName { get; set; }

        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }
        [Display(Name = "Product Selected Quantity")]
        public int ProductSelectedQuantity { get; set; }

        [Display(Name = "Price")]
        public decimal ProductPriceIncludingVat { get; set; }

        public byte[] ProductPhoto { get; set; }

        [Display(Name = "Total price per product")]
        public decimal TotalPricePerOrderDetails { get { return (ProductSelectedQuantity * ProductPriceIncludingVat); } }
    }
}