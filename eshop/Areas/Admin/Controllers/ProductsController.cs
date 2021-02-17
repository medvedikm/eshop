using eshop.Models;
using eshop.Models.Database;
using eshop.Models.DatabaseFake;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class ProductsController : Controller
    {
        IHostingEnvironment Env;
        readonly EshopDBContext EshopDBContext;

        public ProductsController(EshopDBContext eshopDBContext, IHostingEnvironment env)
        {
            this.EshopDBContext = eshopDBContext;
            this.Env = env;
        }

        public async Task<IActionResult> Select()
        {
            ProductViewModel product = new ProductViewModel();
            product.Products = await EshopDBContext.Products.ToListAsync();
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.ImageSrc = String.Empty;

                FileUpload fup = new FileUpload(Env.WebRootPath, "image", "Products");
                product.ImageSrc = await fup.FileUploadAsync(product.Image);

                EshopDBContext.Products.Add(product);

                await EshopDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(Select));
            }
            else
            {
                return View(product);
            }
        }

        public IActionResult Edit(int id)
        {
            Product productItem = EshopDBContext.Products.Where(p => p.ID == id).FirstOrDefault();

            if(productItem != null)
            {
                return View(productItem);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                Product productItem = EshopDBContext.Products.Where(p => p.ID == product.ID).FirstOrDefault();

                if (productItem != null)
                {
                    productItem.ProductName = product.ProductName;
                    productItem.ImageAlt = product.ImageAlt;
                    productItem.Description = product.Description;
                    productItem.Price = product.Price;

                    FileUpload fup = new FileUpload(Env.WebRootPath, "image", "Products");
                    if (String.IsNullOrWhiteSpace(product.ImageSrc = await fup.FileUploadAsync(product.Image)) == false)
                    {
                        productItem.ImageSrc = product.ImageSrc;
                    }

                    await EshopDBContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Select));
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return View(product);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product productItem = EshopDBContext.Products.Where(p => p.ID == id).FirstOrDefault();

            if (productItem != null)
            {
                EshopDBContext.Products.Remove(productItem);
                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Select));
            }
            else
            {
                return NotFound();
            }
        }

    }
}
