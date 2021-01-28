using ScanIT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScanIT.ViewModels
{
    public class OrderVM
    {
        public string ApplicationUserId { get; set; }

        public IEnumerable<OrderDetailsVM> OrderDetailsVMs { get; set; }

        //The following properties might not be included in this simple version

        //public IEnumerable<PaymentStatus> PaymentStatus { get; set; }

        //public IEnumerable<OrderStatus> OrderStatus { get; set; }

        [Required]
        public int PaymentMethodsId { get; set; }

        [Display(Name = "Pay via: ")]
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }

        //Sum the total price per product in order to calculate the cost of the order
        /*        public decimal TotalBasketPrice { 
                    get { return (OrderDetailsVMs.Sum(x => x.TotalPricePerOrderDetails)); } 
                }*/
    }
}