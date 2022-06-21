using MbmStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MbmStore.Models
{
    public class Movie: Product
    {
        // properties
        public string Director { get; set; }
        public short Released { get; set; }

        // constructors
        public Movie() { }

        public Movie(int productId, string title, decimal price) : base (productId, title, price) { }

        public Movie(int productId, string title, decimal price, string imageUrl, string director) : base(productId, title, price)
        {
            ImageUrl = imageUrl;
            Director = director;
        }
    }
}