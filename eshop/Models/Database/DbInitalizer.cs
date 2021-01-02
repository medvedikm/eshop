using eshop.Models.DatabaseFake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public class DbInitializer
    {
        public static void Initialize(EshopDBContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Carousels.Count() == 0) 
            {
                IList<Carousel> carousels = CarouselHelper.GenerateCarousels();
                foreach (var item in carousels)
                    dbContext.Carousels.Add(item);

                dbContext.SaveChanges();
            }

            if (dbContext.Carousels.Count() == 0)
            {
                IList<Product> products = ProductHelper.GenerateProducts();
                foreach (var item in products)
                    dbContext.Products.Add(item);

                dbContext.SaveChanges();
            }
        }
    }
}
