using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScanIT.Models;
using ScanIT.ViewModels;
using System.IO;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using ScanIT.Models.Roles;

namespace ScanIT.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private AppUsersDbContext _context = new AppUsersDbContext();

        [AllowAnonymous]
        public ActionResult Index(string sortOrder, string categoryFilter, string dietaryFilter, string nameFilter)
        {

            // check which works the way we want to : include IsInactive == false or where IsInactive == false 
            //var products = _context.Products.Include(p => p.Category).Include(y=>y.IsInactive == false).ToList();
            var products = _context.Products.Where(p => p.IsInactive == false).Include(p => p.Category).ToList();
            var dietaries = _context.Dietaries.ToList();
            var categories = _context.Categories.ToList();
            var productDietaries = _context.ProductDietaries.ToList();

            //viewbags for sorting
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "ProductName" : "";
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "Price" : "";

            var viewModel = new ProductsDetailsVM();
            //Sorting with actionlinks
            switch (sortOrder)
            {
                case "ProductName":
                    {
                        viewModel.Products = products.OrderBy(p => p.ProductName);
                        viewModel.Dietaries = dietaries;
                        viewModel.Categories = categories;
                        viewModel.ProductDietaries = productDietaries;
                    }
                    break;
                case "Price":
                    {
                        viewModel.Products = products.OrderBy(p => p.PriceIncludingVAT);
                        viewModel.Dietaries = dietaries;
                        viewModel.Categories = categories;
                        viewModel.ProductDietaries = productDietaries;
                    }
                    break;
                default:
                    {
                        viewModel.Products = products.ToList();
                        viewModel.Dietaries = dietaries;
                        viewModel.Categories = categories;
                        viewModel.ProductDietaries = productDietaries;
                    }
                    break;
            }

            //Filters
            ViewBag.CategoryFilter = categories.Select(x => x.CategoryName).Distinct().ToList();
            ViewBag.DietaryFilter = dietaries.Select(x => x.DietaryName).Distinct().ToList();
            var filteredDietaries = productDietaries.Where(d => d.Dietary.DietaryName.Contains(dietaryFilter));
            var tempList = new List<Product>();
            if (!String.IsNullOrEmpty(categoryFilter) && String.IsNullOrEmpty(dietaryFilter))
            {
                viewModel.Products = products.Where(p => p.Category.CategoryName.Contains(categoryFilter));
            }
            else if (String.IsNullOrEmpty(categoryFilter) && !String.IsNullOrEmpty(dietaryFilter))
            {




                foreach (var item in filteredDietaries)
                {
                    tempList.Add(products.SingleOrDefault(p => p.Id == item.ProductId));
                }

                viewModel.Products = tempList;


            }
            else if (!String.IsNullOrEmpty(categoryFilter) && !String.IsNullOrEmpty(dietaryFilter))
            {
                foreach (var item in filteredDietaries)
                {
                    tempList.Add(products.SingleOrDefault(p => p.Id == item.ProductId));
                }
                viewModel.Products = tempList.Where(p => p.Category.CategoryName.Contains(categoryFilter));
            }

            if (User.IsInRole(RoleModel.Manager) || User.IsInRole(RoleModel.Admin))
            {
                ViewBag.Name = "Manage Products";
                return PartialView("_IndexManager", viewModel);
            }

            ViewBag.Name = "Products";
            return PartialView("_IndexUser", viewModel);
        }

        // GET: Products/Details/5
        [Authorize(Roles = RoleModel.ValidatedUser)]
        public ActionResult Details(int? id) //public async Task<ActionResult> Details(int? id) do we need it to be async?
        {

            if (id == null)
            {
                return RedirectToAction("Index"); //return HttpNotFound();
            }

            var product = _context.Products.Find(id);
            //var productDietaries = _context.ProductDietaries.Where(p => p.ProductId == product.Id).Select(z => z.Dietary).ToList();
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            var categories = _context.Categories.ToList();
            var dietaries = _context.Dietaries.ToList();
            var viewModel = new ProductVM()
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                AvailableQuantity = product.AvailableQuantity,
                Photo = product.Photo,
                InitialPrice = product.InitialPrice,
                CategoryVAT = product.Category.VAT,
                BarCode = product.BarCode,
                CategoryId = product.CategoryId,
                Categories = categories,
                Dietaries = dietaries
            };



            //Product product = await _context.Products.FindAsync(id);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "CategoryName");
            var viewModel = new ProductVM()
            {
                Dietaries = _context.Dietaries.ToList(),
                Categories = _context.Categories.ToList()
            };

            return View(viewModel);
        }

        #region Creation of Products

        // Another Idea for the Create Method
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductVM productVM)
        {

            //Getting the files that user has uploaded




            HttpPostedFileBase photoFile = Request.Files["ProductImageData"];
            HttpPostedFileBase barcodeFile = Request.Files["ProductBarcodeData"];
            int i = UploadProductInDataBase(photoFile, barcodeFile, productVM);
            if (i == 1)
            {
                return RedirectToAction("Index");//if i = 1, redirection to index page since the creation was executed successfully
            }

            productVM.Dietaries = _context.Dietaries.ToList();
            productVM.Categories = _context.Categories.ToList();

            return View("Create", productVM);

        }

        public int UploadProductInDataBase(HttpPostedFileBase photoFile, HttpPostedFileBase barcodeFile, ProductVM productVM)
        {
            //creating a product variable in order to pass in it productVM's values and then store the product to database
            var product = new Product();
            //product.Id = productVM.ProductId; //Propably not needed
            product.ProductName = productVM.ProductName;
            product.AvailableQuantity = productVM.AvailableQuantity;
            product.Description = productVM.Description;
            product.InitialPrice = productVM.InitialPrice;
            product.CategoryId = productVM.CategoryId;
            product.Photo = ConvertToBytes(photoFile);
            product.BarCode = ConvertToBytes(barcodeFile);


            /*            var productDietary = new ProductDietary();

                        foreach (var item in productVM.DietariesIds)
                        {
                            productDietary.ProductId = productVM.ProductId;
                            productDietary.DietaryId = item;
                            _context.ProductDietaries.Add(productDietary);

                        }*/



            _context.Products.AddOrUpdate(product); //Do we need modelstate.isvalid?
            int i = _context.SaveChanges();
            foreach (var productDietaryId in productVM.DietariesIds)
            {
                _context.ProductDietaries.Add(new ProductDietary
                {
                    DietaryId = productDietaryId,
                    Dietary = _context.Dietaries.SingleOrDefault(x => x.Id == productDietaryId),
                    Product = product,
                    ProductId = product.Id
                });
            }

            //_context.SaveChanges();

            if (i == 1)
            {
                return (1); // if i equals one then the product has been successfully saved and stored in db
            }
            else
            {
                return (0);
            }
        }
        //Method which converts the uploaded files in byte array in order to store them in database
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            BinaryReader reader = new BinaryReader(image.InputStream);
            byte[] imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        #endregion

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _context.Products.Find(id);
            var viewModel = new ProductVM()
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                AvailableQuantity = product.AvailableQuantity,
                Photo = product.Photo,
                InitialPrice = product.InitialPrice,
                BarCode = product.BarCode,
                CategoryId = product.CategoryId,
                Categories = _context.Categories.ToList(),
                Dietaries = _context.Dietaries.ToList()

            };

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        [Authorize(Roles = RoleModel.Admin + "," + RoleModel.Manager)]
        public ActionResult InactiveProducts(string sortOrder, string categoryFilter, string dietaryFilter, string nameFilter)
        {

            // here we do the same thing as in Index, but we pass the products that are inactive. We didn't do it in the same Action because it was already loaded with a lot of code!
            // maybe that shouldn't be an issue? It just felt cleaner to add another method which is more defined.
            var products = _context.Products.Where(p => p.IsInactive == true).Include(p => p.Category).ToList();
            var dietaries = _context.Dietaries.ToList();
            var categories = _context.Categories.ToList();
            var productDietaries = _context.ProductDietaries.ToList();

            //viewbags for sorting
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "ProductName" : "";
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "Price" : "";

            var viewModel = new ProductsDetailsVM();
            //Sorting with actionlinks
            switch (sortOrder)
            {
                case "ProductName":
                    {
                        viewModel.Products = products.OrderBy(p => p.ProductName);
                        viewModel.Dietaries = dietaries;
                        viewModel.Categories = categories;
                        viewModel.ProductDietaries = productDietaries;
                    }
                    break;
                case "Price":
                    {
                        viewModel.Products = products.OrderBy(p => p.PriceIncludingVAT);
                        viewModel.Dietaries = dietaries;
                        viewModel.Categories = categories;
                        viewModel.ProductDietaries = productDietaries;
                    }
                    break;
                default:
                    {
                        viewModel.Products = products.ToList();
                        viewModel.Dietaries = dietaries;
                        viewModel.Categories = categories;
                        viewModel.ProductDietaries = productDietaries;
                    }
                    break;
            }

            //Filters
            ViewBag.CategoryFilter = categories.Select(x => x.CategoryName).Distinct().ToList();
            ViewBag.DietaryFilter = dietaries.Select(x => x.DietaryName).Distinct().ToList();
            var filteredDietaries = productDietaries.Where(d => d.Dietary.DietaryName.Contains(dietaryFilter));
            var tempList = new List<Product>();
            if (!String.IsNullOrEmpty(categoryFilter) && String.IsNullOrEmpty(dietaryFilter))
            {
                viewModel.Products = products.Where(p => p.Category.CategoryName.Contains(categoryFilter));
            }
            else if (String.IsNullOrEmpty(categoryFilter) && !String.IsNullOrEmpty(dietaryFilter))
            {




                foreach (var item in filteredDietaries)
                {
                    tempList.Add(products.SingleOrDefault(p => p.Id == item.ProductId));
                }

                viewModel.Products = tempList;


            }
            else if (!String.IsNullOrEmpty(categoryFilter) && !String.IsNullOrEmpty(dietaryFilter))
            {
                foreach (var item in filteredDietaries)
                {
                    tempList.Add(products.SingleOrDefault(p => p.Id == item.ProductId));
                }
                viewModel.Products = tempList.Where(p => p.Category.CategoryName.Contains(categoryFilter));
            }

            ViewBag.Name = "Deleted Products";

            return (View(viewModel));
        }


        [Authorize(Roles = RoleModel.Admin + "," + RoleModel.Manager)]
        public ActionResult RestoreProduct(int id)
        {

            var product = _context.Products.Where(y=>y.Id == id).SingleOrDefault();
            product.IsInactive = false;
            _context.Products.AddOrUpdate(product);
            _context.SaveChanges();
            return RedirectToAction("InactiveProducts");
        }


        [HttpPost]
        [Route("Save")]
        public ActionResult Save(ProductVM productVM)
        {
            var productInDb = _context.Products.Find(productVM.ProductId);
            HttpPostedFileBase photoFile = Request.Files["ProductImageData"];
            HttpPostedFileBase barcodeFile = Request.Files["ProductBarcodeData"];
            productInDb.ProductName = productVM.ProductName;
            productInDb.AvailableQuantity = productVM.AvailableQuantity;
            productInDb.Description = productVM.Description;
            productInDb.InitialPrice = productVM.InitialPrice;
            productInDb.CategoryId = productVM.CategoryId;
            var productDietary = new ProductDietary();
            productDietary.ProductId = productInDb.Id;
            productDietary.Product = productInDb;
            var productDietariesAlreadyInDB = _context.ProductDietaries.Where(x => x.ProductId == productInDb.Id); // we find all the productDietaries of our product in our db

            // we remove it manually because there is no method doing that apparently, so foreach and remove
            foreach (var item in productDietariesAlreadyInDB)
            {
                _context.ProductDietaries.Remove(item);
            }

            _context.SaveChanges();


            // check if null to avoid errors
            if (productVM.DietariesIds!=null)
            {            //now we add all the new productDieteries...
                foreach (var item in productVM.DietariesIds)
                {
                    productDietary.DietaryId = item;
                    productDietary.Dietary = _context.Dietaries.SingleOrDefault(p => p.Id == item);


                    _context.ProductDietaries.Add(productDietary);

                    _context.SaveChanges();
                }

            }


            

            if (photoFile.ContentLength != 0)
            {
                productInDb.Photo = ConvertToBytes(photoFile);
            }
            if (barcodeFile.ContentLength != 0)
            {
                productInDb.BarCode = ConvertToBytes(barcodeFile);
            }
            

            _context.Products.AddOrUpdate(productInDb);
 
            _context.SaveChanges();
 
            return RedirectToAction("Index");
     


        }



        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            // instead of deleting the product, we change its status to IsInactive
            Product product = await _context.Products.FindAsync(id);
            product.IsInactive = true;
            //_context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
