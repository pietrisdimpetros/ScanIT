using Microsoft.AspNet.Identity.EntityFramework;
using ScanIT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScanIT.ViewModels
{
    public class AdminVM
    {

        [Required]
        public string UserId { get; set; }

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
        [Range(1, 99999)]
        public int ZipCode { get; set; }

        #endregion

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public bool EmailConfirmed { get; set; }

        public string RoleId { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}