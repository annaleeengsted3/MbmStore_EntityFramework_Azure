using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace MbmStore.Models {

    public class Product {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public string Category { get; set; }
       
        public DateTime? CreatedDate { get; set; }


        // constructor
        public Product() { }

        // constructor
        public Product(int productId, string title, decimal price) {
            ProductId = productId;
            Title = title;
            Price = price;
        }
    }
}