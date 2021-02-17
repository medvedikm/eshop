using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Models;
using eshop.Models.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace eshop.Controllers
{
    public class ProductsController : Controller
    {
        readonly EshopDBContext EshopDBContext;

        public ProductsController(EshopDBContext eshopDBContex)
        {
            this.EshopDBContext = eshopDBContex;

        }
        public IActionResult Detail(int id)
        {
            Product product = EshopDBContext.Products.Where(p => p.ID == id).FirstOrDefault();
            if (product != null)
            {
                return View(product);
                // tady spravit :-D 
            }
            else
            {
                return NotFound();
            }
        }
    }
}
