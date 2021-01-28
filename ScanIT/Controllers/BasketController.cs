using Microsoft.AspNet.Identity;
using ScanIT.Models;
using ScanIT.Models.Roles;
using ScanIT.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScanIT.Controllers
{
    public class BasketController : Controller
    {
        private AppUsersDbContext _context = new AppUsersDbContext();

        [HttpPost]
        
        public ActionResult AddToBasket(ProductVM productVM)
        {
            //When a product is added in the basket, orderdetails should be updated.
            //A new orderdetails should be created in order to pass on the values in the database
            var orderDetails = new OrderDetails();
            var product = _context.Products.Find(productVM.ProductId);
            var categories = _context.Categories.ToList();
            var dietaries = _context.Dietaries.ToList();
            //In order to pass on the value of orderID, it is needed to be checked whether the user has any orders ongoing or a new one must be created
            var userId = User.Identity.GetUserId();

            orderDetails.ProductId = product.Id;
            orderDetails.Product = _context.Products.SingleOrDefault(p => p.Id == product.Id);
            orderDetails.Price = product.PriceIncludingVAT;
            orderDetails.SelectedQuantity = productVM.SelectedQuantity;




            var openOrders = _context.Orders.Where(o => o.OrderStatusId == 1).ToList();
            var usersOpenOrder = openOrders.SingleOrDefault(o => o.ApplicationUserId == userId);
            bool twoSameProducts = false;

            //I splitted the linq because for some reason it did not recognize the variable as a list of orders but as IQuerable
            //In this condition we test whether there are open orders in order to create them or use the already opened one.
            if (openOrders.Count() == 0)
            {
                var order = new Order();
                order.ApplicationUserId = userId;
                _context.Orders.AddOrUpdate(order);
                _context.SaveChanges();
                orderDetails.OrderId = order.Id;
                orderDetails.Order = _context.Orders.SingleOrDefault(o => o.Id == order.Id);
            }
            else if (openOrders.Count() != 0 && usersOpenOrder == null)
            {
                var order = new Order();
                order.ApplicationUserId = userId;
                _context.Orders.AddOrUpdate(order);
                _context.SaveChanges();
                orderDetails.OrderId = order.Id;
                orderDetails.Order = _context.Orders.SingleOrDefault(o => o.Id == order.Id);
            }
            else if (openOrders.Count() != 0 && usersOpenOrder != null)
            {
                var orderIDAlreadyExisting = openOrders.Where(o => o.ApplicationUserId == userId).Select(k => k.Id).SingleOrDefault();
                //if the order already exists then we assign the orderId already existing.
                orderDetails.OrderId = orderIDAlreadyExisting;
                //Selecting existing orderdetails
                var orderDetailsAlreadyExisting = _context.OrdersDetails.Where(o => o.OrderId == orderIDAlreadyExisting);
                var orderDetailsOfTheProductIncludedInTheSpecificOrder = orderDetailsAlreadyExisting.Where(x => x.ProductId == product.Id).SingleOrDefault();

                //if the product already exists in our order then we update its selected quantity
                if (orderDetailsOfTheProductIncludedInTheSpecificOrder != null)
                {
                    orderDetailsOfTheProductIncludedInTheSpecificOrder.SelectedQuantity += productVM.SelectedQuantity;

                    if (product.AvailableQuantity - orderDetailsOfTheProductIncludedInTheSpecificOrder.SelectedQuantity >= 0)
                    {
                        
                        _context.OrdersDetails.AddOrUpdate(orderDetailsOfTheProductIncludedInTheSpecificOrder);
                        _context.SaveChanges();
                    }
                    else
                    {
                        TempData["message"] = $"The extra quantity you have selected exceeds the available quantity of {product.ProductName}. Remaining quantity : {product.AvailableQuantity - orderDetailsOfTheProductIncludedInTheSpecificOrder.SelectedQuantity + productVM.SelectedQuantity }";
                        return RedirectToAction("Index", "Products");
                    }
                    twoSameProducts = true;
                }
            }

            //If true it means that orderdetails does not exist in order
            if (!twoSameProducts)
            {
                orderDetails.ProductId = product.Id;
                orderDetails.Product = _context.Products.SingleOrDefault(p => p.Id == product.Id);
                orderDetails.Price = product.PriceIncludingVAT;
                orderDetails.SelectedQuantity = productVM.SelectedQuantity;
                
                _context.OrdersDetails.AddOrUpdate(orderDetails);
                _context.SaveChanges();
            }
            TempData["message"] = $"{product.ProductName} has just been added in your cart!";
            return RedirectToAction("Index", "Products");
        }

        public ActionResult DeleteProduct(string productName, string userId)
        {
            //if there is no product to be deleted or no order matching we redirect
            if (String.IsNullOrWhiteSpace(productName) || String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToAction("ProceedToCheckOut");
            }

            // find the junction table of the order with the product and change its value 

            var order = _context.Orders.SingleOrDefault(y => y.ApplicationUserId == userId && y.OrderStatusId == 1);
            var product = _context.Products.FirstOrDefault(y => y.ProductName == productName);
            var orderDetails = _context.OrdersDetails.SingleOrDefault(y => y.OrderId == order.Id && y.ProductId == product.Id);
            _context.OrdersDetails.Remove(orderDetails);
            _context.SaveChanges();

            var checkOrder = _context.OrdersDetails.Where(y => y.OrderId == order.Id);


            if (checkOrder.Count() == 0)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
            return RedirectToAction("ProceedToCheckOut");
        }


        public ActionResult ProceedToCheckOut()
        {
            //In general due to the fact that user is connected only to one ongoing order, we can easily trace the required information. We do not need to pass on orderId etc.
            //If we pass on the corresponding ids then we just need to use "find" function.
            var userId = User.Identity.GetUserId();

            #region Chrome Bug: Two open orders simioutanesly (Comments - To be further investigated)

            /* var test = _context.Orders.Where(g => g.OrderStatusId == 1).Select(y => y.ApplicationUserId == userId).ToList();

             //catching the error where user has two oppened orders (google cache has caused this error)

             if (test.Count() > 1)
             {
                 _context.Orders.Remove(_context.Orders.Where(g => g.OrderStatusId == 1).First(y => y.ApplicationUserId == userId));
                 _context.SaveChanges();
             }*/

            #endregion

            //finding the order in which orderdetails are included
            var orderProceedingToCheckOut = _context.Orders.Where(g => g.OrderStatusId == 1).SingleOrDefault(o => o.ApplicationUserId == userId);

            //User is not permitted to access his basket if no products are included. Thus, he gets the following message.
            if (orderProceedingToCheckOut == null)
            {
                TempData["message"] = "Your basket is empty. Please add products first.";
                return RedirectToAction("Index", "Products");
            }

            //Collecting the orderdetails that this order includes
            var orderDetailsProceedingToCheckOut = _context.OrdersDetails.Where(o => o.OrderId == orderProceedingToCheckOut.Id).ToList();

            var listOfIdsDerivedFromProductsIncludedInOrderDetails = orderDetailsProceedingToCheckOut.Select(o => o.ProductId).ToList();

            var orderVM = new OrderVM();

            //We are creating a templist in order to assign it later
            var tempList = new List<OrderDetailsVM>();


            //The counter below is used in order to access properly the selected quantity of orderDetailsProceedingToCheckOut
            int i = 0;
            //In the foreach we create a temp variable in order to populate the OrderDetailsVM 
            foreach (var item in listOfIdsDerivedFromProductsIncludedInOrderDetails)
            {
                var orderDetailsVM = new OrderDetailsVM();
                orderDetailsVM.ProductName = _context.Products.Where(p => p.Id == item).Select(k => k.ProductName).SingleOrDefault();
                orderDetailsVM.ProductDescription = _context.Products.Where(p => p.Id == item).Select(k => k.Description).SingleOrDefault();
                orderDetailsVM.ProductPhoto = _context.Products.Where(p => p.Id == item).Select(k => k.Photo).SingleOrDefault();

                //We retrieve initial price and VAT seperatelly due to the fact that we cannot access a property that is not set in the database "PriceIncludingVAT"
                var initialPrice = _context.Products.Where(p => p.Id == item).Select(k => k.InitialPrice).SingleOrDefault();
                var vAT = _context.Products.Where(p => p.Id == item).Select(k => k.Category.VAT).SingleOrDefault();
                orderDetailsVM.ProductPriceIncludingVat = (1 + vAT) * initialPrice;

                orderDetailsVM.ProductSelectedQuantity = orderDetailsProceedingToCheckOut[i].SelectedQuantity;
                tempList.Add(orderDetailsVM);
                i++;
            }
            //THIS IS A BASIC ASSIGN FOR THE VM. 
            orderVM.OrderDetailsVMs = tempList;

            //THIS IS A BASIC ASSIGN FOR THE VM.
            orderVM.PaymentMethods = _context.PaymentMethods.ToList();

            //THIS IS A BASIC ASSIGN FOR THE VM.
            orderVM.ApplicationUserId = userId;

            return View(orderVM);
        }

        [HttpPost]
        public void CompleteShopping(string status)
        {
            var userId = User.Identity.GetUserId();
            var orderProceedingToCheckOut = _context.Orders.SingleOrDefault(o => o.ApplicationUserId == userId && o.OrderStatusId == 1);

            //Update Quantities
            var products = _context.Products.ToList();
            var orderDetailsCompleted = _context.OrdersDetails.Where(o => o.OrderId == orderProceedingToCheckOut.Id).ToList();
            var productsIdsNeedToBeUpdated = orderDetailsCompleted.Select(o => o.Product).ToList();

            for (int i = 0; i < productsIdsNeedToBeUpdated.Count(); i++)
            {
                    productsIdsNeedToBeUpdated[i].AvailableQuantity -= orderDetailsCompleted[i].SelectedQuantity;
            }

            //if true then the order is valid
            if (status!= null)
            {
                //Change order from ongoing to completed
                orderProceedingToCheckOut.OrderStatusId = 2;
                _context.Products.AddOrUpdate();
                _context.SaveChanges();
            }

        }
        public ActionResult DropOrder()
        {
            var userId = User.Identity.GetUserId();
            var orderToBeDeleted = _context.Orders.SingleOrDefault(o => o.ApplicationUserId == userId && o.OrderStatusId == 1);           
            var orderDetailsToBeDeleted = _context.OrdersDetails.Where(o => o.OrderId == orderToBeDeleted.Id).ToList();
            foreach (var item in orderDetailsToBeDeleted)
            {
                _context.OrdersDetails.Remove(item);
                _context.SaveChanges();//To be further investigated, save changes in a loop? 
            }

            _context.Orders.Remove(orderToBeDeleted);
            _context.SaveChanges();

            ViewBag.MessageTitle = "Your order has been deleted!";
            ViewBag.Message = "If you want to continue shopping, please click the link below. Otherwise we wish you a wonderful day and we hope to see you again!";

            return View("ThankYouScreen");
        }

        public ActionResult ThankYou()
        {
            ViewBag.MessageTitle = "Thank You for your Purchace!";
            ViewBag.Message = "Your purchase has been saved. We would love to see you again!";

            return View("ThankYouScreen");
        }
    }

}