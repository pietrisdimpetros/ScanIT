using ScanIT.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScanIT.CustomValidations
{
    public class SelectedQuantityValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var productVM = (ProductVM)validationContext.ObjectInstance;
            var availableQuantity = productVM.AvailableQuantity;
            var selectedQuantity = productVM.SelectedQuantity;

            /*            if (orderDetail.SelectedQuantity < availableQuantity)
                        {
                            return ValidationResult.Success;
                        }
                        else
                        {
                            return new ValidationResult("The selected quantity exceeds the available quantity in stock.");
                        }*/
            return (selectedQuantity < availableQuantity)? ValidationResult.Success : new ValidationResult("The selected quantity exceeds the available quantity in stock.");
        }
    }
}