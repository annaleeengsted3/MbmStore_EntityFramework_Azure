using MbmStore.Data;
using MbmStore.Models;
using MbmStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MbmStore.Controllers
{
    public class OrderController : Controller
    {
        private Cart cart;
        private MbmStoreContext dataContext;

        public OrderController(Cart cartService, MbmStoreContext dbContext)
        {
            cart = cartService;
            dataContext = dbContext;
        }


        public ViewResult Checkout()
        {
            return View(new Order());
        }


        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (cart.Lines.Count == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
           if (ModelState.IsValid)
            {
                // order processing logic
                Customer customer = new Customer
                {
                    Firstname = order.Firstname,
                    Lastname = order.Lastname,
                    Address = order.Line1 + " " + order.Line2 + " " + order.Line3,
                    City = order.City, 
                    Zip = order.Zip,
                    Email = order.Email
                };


                if (dataContext.Customers.Any(c => c.Firstname == customer.Firstname && c.Lastname == customer.Lastname && c.Email == customer.Email))
                {
                    customer = dataContext.Customers
                        .Where(c => c.Firstname == customer.Firstname 
                        && c.Lastname == customer.Lastname 
                        && c.Email == customer.Email).First(); 
                    customer.Address = order.Line1 + " " + order.Line2 + " " + order.Line3; 
                    customer.Zip = order.Zip; 
                    // ensure update instead of insert 
                    dataContext.Entry(customer).State = EntityState.Modified; 
                }
                Invoice invoice = new Invoice { Customer = customer, OrderDate = DateTime.Now };
                foreach (CartLine cartLine in cart.Lines) { 
                    OrderItem orderItem = new OrderItem(cartLine.Product, cartLine.Quantity); 
                    orderItem.ProductId = cartLine.Product.ProductId; 
                    orderItem.Product = null; 
                    invoice.OrderItems.Add(orderItem); 
                }

                // Save to data store
                dataContext.Invoices.Add(invoice); 
                await dataContext.SaveChangesAsync();

                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(order);
            }
        }
    }
}