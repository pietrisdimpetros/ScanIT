using ScanIT.Models;
using ScanIT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScanIT.Controllers
{
    public class RecipesController : Controller
    {

        private AppUsersDbContext _context = new AppUsersDbContext();
        // GET: Recipes
        //with this view we will display the products that are included in one specific recipre
        //this method gets the id of the junction table in order to trace the recipe and the corresponding products

        public ActionResult Index(int? productRecipeId)
        {
            var recipeDetailsVM = new RecipeDetailsVM();
            return RedirectToAction("Index", new { productRecipeId = productRecipeId, recipeDetailsVM = recipeDetailsVM });
        }




        public ActionResult Index(int? productRecipeId, RecipeDetailsVM recipeDetailsVM)
        {
            //Selecting the recipe from the junction table
            var selectedRecipe = _context.ProductsRecipes.Where(y => y.RecipeId == productRecipeId).ToList();


            if (recipeDetailsVM != null)
            {
                return View(recipeDetailsVM);
            }
            else
            {
                //creating an viewmodel object and passing the values from the database
                //we hope that if the variable is null wont be a problem :P 
                RecipeDetailsVM recipeDetailsVM2 = new RecipeDetailsVM();
                var recipe = _context.Recipes.Where(y => y.Id == productRecipeId).SingleOrDefault();
                recipeDetailsVM2.RecipeName = recipe.RecipeName;
                recipeDetailsVM2.PreparationTime = recipe.PreparationTime;
                recipeDetailsVM2.ExecutionTime = recipe.ExecutionTime;
                recipeDetailsVM2.DifficultyLevel = recipe.DifficultyLevel;
                recipeDetailsVM2.RecipeId = productRecipeId;


                var tempListProductVMs = new List<ProductVM>();
                var productVM = new ProductVM();

                foreach (var item in selectedRecipe.Select(y => y.ProductId))
                {
                    var product = _context.Products.Find(item);
                    productVM.ProductName = product.ProductName;
                    productVM.Description = product.Description;
                    productVM.AvailableQuantity = product.AvailableQuantity;
                    productVM.BarCode = product.BarCode;
                    productVM.CategoryId = product.CategoryId;
                    productVM.InitialPrice = product.InitialPrice;
                    productVM.Photo = product.Photo;
                    productVM.Dietaries = _context.Dietaries.ToList();
                    productVM.DietariesIds = new int[] { 1, 2, 3 };
                    productVM.Categories = _context.Categories.ToList();
                    productVM.CategoryId = product.CategoryId;
                    productVM.CategoryVAT = 0.00m;
                    productVM.ProductId = product.Id;
                    tempListProductVMs.Add(productVM);
                }

                recipeDetailsVM2.ProductVMs = tempListProductVMs;

                return View(recipeDetailsVM2);
            }
        }

        //When a product is removed the list of products in the VM must to be updated before it is passed to basket
        public ActionResult RemoveProduct(int? productID, RecipeDetailsVM recipeDetailsVM)
        {
            if (productID != null)
            {
                recipeDetailsVM.ProductVMs.ToList().Remove(recipeDetailsVM.ProductVMs.Where(x => x.ProductId == productID).SingleOrDefault());
                return RedirectToAction("Index", recipeDetailsVM);
            }
            else
            {
                return HttpNotFound();
            }
        }

        //with this we will be adding the products in the basket in order for the client to buy them
        //one VM model already created in the Index method
        public void AddRecipeToBasket(RecipeDetailsVM recipeDetailsVM)
        {
            foreach (var item in recipeDetailsVM.ProductVMs)
            {
                RedirectToAction("AddToBasket", "Basket", item);
            }
        }
    }
}