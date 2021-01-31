using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.DatabaseFake
{
    public static class CarouselHelper
    {
        public static IList<Carousel> GenerateCarousels()
        {
            IList<Carousel> carousels = new List<Carousel>()
            {
                new Carousel() {
                    DataTarget="#myCarousel",
                    ImageSrc="/images/Products/car1.jpg",
                    ImageAlt="First car",
                    CarouselContent="Our awesome products will make you happy forever!"
                },

                new Carousel() {
                    DataTarget="#myCarousel",
                    ImageSrc="/images/Products/car2.jfif",
                    ImageAlt="Second car",
                    CarouselContent="We have the best choice around the world!"
                },

                new Carousel() {
                    DataTarget="#myCarousel",
                    ImageSrc="/images/car4.png",
                    ImageAlt="Third car",
                    CarouselContent="Our technician team will make everything to fullfil your wishes!"
                },

            };

            return carousels;
        }
    }
}
