using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ScanIT.Models;
using ScanIT.Models.Roles;
using ScanIT.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScanIT.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private AppUsersDbContext _context;

        public UsersController()
        {
            this._context = new AppUsersDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize(Roles = RoleModel.Admin)]

        public ActionResult AdminPage()
        {
            var users = _context.Users.ToList();

            return View(users);
        }


        [Authorize(Roles = RoleModel.Admin)]
        public ActionResult AdminEdit(string id)
        {
            //we find the user from the given id, then we find their role, we use foreach because roles are a IEnumerable but we don't have this problem
            //because we only assign one role to one user(when we change a users role at the AdminSave method, we are using Roles.Clear so every time we
            //only have one role. 
            var user = _context.Users.Include(x => x.Roles).SingleOrDefault(c => c.Id == id);
            var roleStore = new RoleStore<IdentityRole>(new AppUsersDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userRoleIds = user.Roles.Select(y => y.RoleId);
            string userRoleId = "";
            foreach (var item in userRoleIds)
            {
                userRoleId = item;
            }
            if (user != null)
            {
                var viewModel = new AdminVM
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    Country = user.Country,
                    Region = user.Region,
                    City = user.City,
                    Street = user.Street,
                    StreetNumber = user.StreetNumber,
                    ZipCode = user.ZipCode,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    UserName = user.UserName,
                    RoleId = userRoleId,
                    Roles = _context.Roles//.ToList()
                };

                ViewBag.Name = "Edit Profile";
                return View("AdminEdit", viewModel);
            }
            ViewBag.Alert = "This Profile Doesn't Exist";
            return View("AdminPage");
        }
        public ActionResult Chat()
        {
            var userId = User.Identity.GetUserId();

            var user = _context.Users.SingleOrDefault(y => y.Id == userId);

            var viewModel = new ChatTestVM()
            {
                DisplayName = $"{user.FirstName} {user.LastName}"
            };

            ViewBag.DisplayName = $"{user.FirstName} {user.LastName}";

            if (String.IsNullOrWhiteSpace(userId))
            {
                ViewBag.Alert = "You have to be logged in to access the chat!";
                RedirectToAction("Login", "Account");
            }
            return (View(viewModel));
        }

        [Authorize]
        public ActionResult Edit(string id)
        {
            var user = _context.Users.SingleOrDefault(c => c.Id == id);
            if (user != null)
            {
                var viewModel = new UserInfoVM
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    GenderId = user.GenderId,
                    Country = user.Country,
                    Region = user.Region,
                    City = user.City,
                    Street = user.Street,
                    StreetNumber = user.StreetNumber,
                    ZipCode = user.ZipCode,
                    Genders = _context.Genders.ToList()
                };

                ViewBag.Name = "Edit Profile";
                return View("UserProfile", viewModel);
            }
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = RoleModel.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminSave(AdminVM userInfo)
        {
            if (!ModelState.IsValid)
            {
                return View("AdminEdit", userInfo);
            }

            else
            {
                var userInDb = _context.Users.Find(userInfo.UserId);
                _ = _context.Roles;
                var roleStore = new RoleStore<IdentityRole>(new AppUsersDbContext());
                var roleManager = new RoleManager<IdentityRole>(roleStore); ;

                userInDb.Id = userInfo.UserId;
                userInDb.FirstName = userInfo.FirstName;
                userInDb.LastName = userInfo.LastName;
                userInDb.DateOfBirth = userInfo.DateOfBirth;
                userInDb.Country = userInfo.Country;
                userInDb.Region = userInfo.Region;
                userInDb.City = userInfo.City;
                userInDb.Street = userInfo.Street;
                userInDb.StreetNumber = userInfo.StreetNumber;
                userInDb.ZipCode = userInfo.ZipCode;
                userInDb.Email = userInfo.Email;
                userInDb.EmailConfirmed = userInfo.EmailConfirmed;
                userInDb.UserName = userInfo.UserName;
                userInDb.Roles.Clear();

                var role = roleManager.FindById(userInfo.RoleId);
                roleManager.Create(new IdentityRole(role.Name));
                HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().AddToRole(userInDb.Id, role.Name);

                _context.Entry(userInDb).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("AdminPage", "Users");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(UserInfoVM userInfo)
        {
            if (!ModelState.IsValid)
            {

                return View("UserProfile", userInfo);

            }


            //else if (String.IsNullOrEmpty(user.Id)
            //{ _context.Users.Add(user); }

            else
            {

                //return Content($"user first name : {user.FirstName}, user last name : {user.LastName}, birthdade : {user.DateOfBirth}");
                var userInDb = _context.Users.Single(c => c.Id == userInfo.UserId);

                userInDb.FirstName = userInfo.FirstName;
                userInDb.LastName = userInfo.LastName;
                userInDb.DateOfBirth = userInfo.DateOfBirth;
                userInDb.GenderId = userInfo.GenderId;
                userInDb.Country = userInfo.Country;
                userInDb.Region = userInfo.Region;
                userInDb.City = userInfo.City;
                userInDb.Street = userInfo.Street;
                userInDb.StreetNumber = userInfo.StreetNumber;
                userInDb.ZipCode = userInfo.ZipCode;




                _context.Entry(userInDb).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index", "Users");
            }

            //userInDb.PasswordHash = user.PasswordHash;
            //userInDb.Id = user.Id;
            //userInDb.PhoneNumber = user.PhoneNumber;
            //userInDb.AccessFailedCount = user.AccessFailedCount;
            //userInDb.EmailConfirmed = user.EmailConfirmed;
            //userInDb.LockoutEnabled = user.LockoutEnabled;
            //userInDb.LockoutEndDateUtc = user.LockoutEndDateUtc;
            //userInDb.Email = user.Email;
            //userInDb.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            //userInDb.UserName = user.UserName;
            //userInDb.SecurityStamp = user.SecurityStamp;
            //userInDb.TwoFactorEnabled = user.TwoFactorEnabled;





            //TryUpdateModel(userInDb);  // There are some issues with this approach : It opens security holes!


        }

        [Authorize]
        public ActionResult Index()
        {

            var profileId = User.Identity.GetUserId();

            var userProfile = _context.Users.Include(x => x.Gender).Single(u => u.Id == profileId);

            if (User.IsInRole(RoleModel.Admin))
            {
                ViewBag.Name = "Admin Page";
                return PartialView("_IndexAdmin", userProfile);
            }
            if (userProfile != null)
            {
                return View(userProfile);
            }
            else
            {

                ViewBag.Name = "You have to login to access this page";
                return RedirectToAction("Login", "Account");
            }

            //var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            //if (customers.Count == 0)
            //{

            //    ViewBag.Name = "There are no Customers Yet!";
            //    return View();
            //}

            //return View(customers);
        }

        //public ActionResult Details(int? id)
        //{
        //    var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {

        //        return View(customer);
        //    }
        //}
    }
}