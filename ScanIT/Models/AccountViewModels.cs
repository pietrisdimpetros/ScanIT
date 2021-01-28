using ScanIT.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScanIT.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        #region UserInfo
        [Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is Required")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        #endregion
        #region location
        [Required(ErrorMessage = "Country is Required")]
        [RegularExpression(@"^[a-zA-Z ]*[Α-Ωα-ωίϊΐόάέύϋΰήώΏΆΈΊΉΌΎΪΫ ]*$", ErrorMessage = "Enter a valid Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Region is Required")]
        [RegularExpression(@"^([a-zA-Z\u0080-\u024F ]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]+[Α-Ωα-ωίϊΐόάέύϋΰήώΏΆΈΊΉΌΎΪΫ ]*$", ErrorMessage = "Enter a valid Region")]
        public string Region { get; set; }

        [Required(ErrorMessage = "City is Required")]
        [RegularExpression(@"^([a-zA-Z\u0080-\u024F ]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]+[Α-Ωα-ωίϊΐόάέύϋΰήώΏΆΈΊΉΌΎΪΫ ]*$", ErrorMessage = "Enter a valid City")]
        [Display(Name = "City/Town/Village")]
        public string City { get; set; }

        [Required(ErrorMessage = "Street is Required")]
        [RegularExpression(@"^([a-zA-Z\u0080-\u024F ]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]+[Α-Ωα-ωίϊΐόάέύϋΰήώΏΆΈΊΉΌΎΪΫ ]*$", ErrorMessage = "Enter a valid Street")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Street Number is Required")]
        [RegularExpression(@"^([a-zA-Z+(0-9) ])*[Α-Ωα-ωίϊΐόάέύϋΰήώΏΆΈΊΉΌΎΪΫ+(0-9) ]*$", ErrorMessage = "Enter a valid Street Number")]
        [Display(Name = "Street Number")]
        public string StreetNumber { get; set; }

        [Required(ErrorMessage = "ZIP Code is Required")]
        [RegularExpression(@"\d{5}(-\d{4})?$", ErrorMessage = "Enter a valid 5 digit ZIP code")]

        public int ZipCode { get; set; }

        #endregion


        //public int GenderId { get; set; }

        //// IEnumerable in order to chose from a drop down list
        //public IEnumerable<Gender> Genders { get; set; }

        //public UserInfoVM UserInfoVM { get; set; }
        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
