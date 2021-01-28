using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace ScanIT.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
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
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
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
        [Range(1,99999)]
        public int ZipCode { get; set; }

        #endregion

        [ForeignKey("Gender")]
        [Display(Name = "Gender ")]
        public int? GenderId { get; set; }
        
        public Gender Gender { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class AppUsersDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppUsersDbContext()
            : base("SystemUsers", throwIfV1Schema: false)
        {
        }

        public static AppUsersDbContext Create()
        {
            return new AppUsersDbContext();
        }

        public System.Data.Entity.DbSet<Product> Products { get; set; }

        public System.Data.Entity.DbSet<Category> Categories { get; set; }

        //Models DbSets

        public DbSet<Dietary> Dietaries { get; set; }

        public DbSet<ProductDietary> ProductDietaries { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrdersDetails { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<PaymentStatus> PaymentStatuses { get; set; }

        public DbSet<Gender> Genders{ get; set; }
        public DbSet<Recipe.Recipe> Recipes{ get; set; }
        public DbSet<Recipe.ProductRecipe> ProductsRecipes{ get; set; }
     
        
    }
}