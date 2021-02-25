using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.DatabaseFake
{
    public class ProductHelper
    {
        public static IList<Product> GenerateProducts()
        {
            IList<Product> products = new List<Product>()
            {
                new Product()
                {
                    ProductName = "Bugatti Veyron",
                    ImageSrc = "/images/Products/BugattiVeyron.jfif",
                    ImageAlt = "Bugatti-Veyron",
                    Description = "Bugatti Veyron",
                    Price = 4850000
                },

                new Product()
                {
                    ProductName = "Lamborghini",
                    ImageSrc = "/images/Products/Lamborghini.jpg",
                    ImageAlt = "Lamborghini",
                    Description = "Lamborghini",
                    Price = 2800000
                },

                new Product()
                {
                    ProductName = "Nissan GTR",
                    ImageSrc = "/images/Products/NissanGTR.jpg",
                    ImageAlt = "Nissan-GTR",
                    Description = "Nissan GTR",
                    Price = 1950000
                }

            };
            return products;
        }
    }
}
