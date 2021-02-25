using eshop.Areas.Customer.Controllers;
using eshop.Controllers;
using eshop.Models;
using eshop.Models.ApplicationServices;
using eshop.Models.Database;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = nameof(Roles.Customer))]
    public class CustomerOrderNotCartController : Controller
    {
        const string totalPriceString = "TotalPrice";
        const string orderItemsString = "OrderItems";


        ISecurityApplicationService iSecure;
        EshopDBContext EshopDBContext;
        public CustomerOrderNotCartController(ISecurityApplicationService iSecure, EshopDBContext eshopDBContext)
        {
            this.iSecure = iSecure;
            EshopDBContext = eshopDBContext;
        }


        [HttpPost]
        public double AddOrderItemsToSession(int? productId)
        {
            double totalPrice = 0;

            double discountPrice = 0.9; //10% sleva
            double neededSum = 30000000; //30M

            double discountItems = 0.8;
            int itemCounter = 0;
            int neededItems = 3;

            if (HttpContext.Session.IsAvailable)
            {
                totalPrice = HttpContext.Session.GetDouble(totalPriceString).GetValueOrDefault();
            }

            Product product = EshopDBContext.Products.Where(prod => prod.ID == productId).FirstOrDefault();

            if (product != null)
            {
                OrderItems orderItem = new OrderItems()
                {
                    ProductID = product.ID,
                    Product = product,
                    Amount = 1,
                    Price = (decimal)product.Price   //zde pozor na datový typ -> pokud máte Price v obou případech double nebo decimal, tak je to OK. Mě se bohužel povedlo mít to jednou jako decimal a jednou jako double. Nejlepší je datový typ změnit v databázi/třídě, tak to prosím udělejte.
                };
                

                if (HttpContext.Session.IsAvailable)
                {
                    

                    List<OrderItems> orderItems = HttpContext.Session.GetObject<List<OrderItems>>(orderItemsString);
                    OrderItems orderItemInSession = null;
                    if (orderItems != null) { 
                        orderItemInSession = orderItems.Find(oi => oi.ProductID == orderItem.ProductID);
                        
                    }

                    else { 
                        orderItems = new List<OrderItems>();
                    }

                    if (orderItemInSession != null)
                    {
                        ++orderItemInSession.Amount;
                        

                        orderItemInSession.Price += (decimal)orderItem.Product.Price;   //zde pozor na datový typ -> pokud máte Price v obou případech double nebo decimal, tak je to OK. Mě se bohužel povedlo mít to jednou jako decimal a jednou jako double. Nejlepší je datový typ změnit v databázi/třídě, tak to prosím udělejte.
                    }
                    else
                    {
                        orderItems.Add(orderItem);
                    }

                    HttpContext.Session.SetObject(orderItemsString, orderItems);

                    totalPrice += orderItem.Product.Price;
                    HttpContext.Session.SetDouble(totalPriceString, totalPrice);
                }
            }
            if (itemCounter >= neededItems) totalPrice *= discountItems;

            if (totalPrice >= neededSum) totalPrice *= discountPrice;
            return totalPrice;
        }


        public async Task<IActionResult> ApproveOrderInSession()
        {
            if (HttpContext.Session.IsAvailable)
            {
                double totalPrice = 0;

                double discountPrice = 0.9; //10% sleva
                double neededSum = 30000000; //30M


                List<OrderItems> orderItems = HttpContext.Session.GetObject<List<OrderItems>>(orderItemsString);
                if (orderItems != null)
                {
                    foreach (OrderItems orderItem in orderItems)
                    {
                        totalPrice += orderItem.Product.Price * orderItem.Amount;
                        orderItem.Product = null; //zde musime nullovat referenci na produkt, jinak by doslo o pokus jej znovu vlozit do databaze
                    }

                    User currentUser = await iSecure.GetCurrentUser(User);

                    if (currentUser.IsStudent == true) totalPrice *= currentUser.Sleva;
                    if (totalPrice >= neededSum) totalPrice *= discountPrice;


                    Order order = new Order()
                    {
                        OrderNumber = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                        TotalPrice = totalPrice,
                        OrderItems = orderItems,
                        UserId = currentUser.Id,
                    };



                    //We can add just the order; order items will be added automatically.
                    await EshopDBContext.AddAsync(order);
                    await EshopDBContext.SaveChangesAsync();



                    HttpContext.Session.Remove(orderItemsString);
                    HttpContext.Session.Remove(totalPriceString);

                    return RedirectToAction(nameof(CustomerOrdersController.Index), nameof(CustomerOrdersController).Replace("Controller", ""), new { Area = nameof(Customer) });

                }
            }

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""), new { Area = "" });

        }
    }
}
