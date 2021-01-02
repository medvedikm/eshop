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
            IList<Product> products = new List<Product>
            {
                new Product()
                {
                    ProductName = "Bugatti Veyron",
                    ImageSrc = "/images/BugattiVeyron.jfif",
                    ImageAlt = "Bugatti-Veyron"
                },

                new Product()
                {
                    ProductName = "Lamborghini",
                    ImageSrc = "/images/Lamborghini.jpg",
                    ImageAlt = "Lamborghini"
                },

                new Product()
                {
                    ProductName = "Nissan GTR",
                    ImageSrc = "/images/NissanGTR.jpg",
                    ImageAlt = "Nissan-GTR"
                },

            };
            return products;
        }
    }
}
