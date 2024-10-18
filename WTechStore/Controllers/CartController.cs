using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WTechStore.Models;

namespace WTechStore.Controllers
{
    public class CartController : Controller
    {
        // In-memory cart for demonstration; use session or database for production
        private static List<Cart> CartItems = new List<Cart>();

        // Action to display the cart
        public IActionResult Index()
        {
            return View(CartItems);
        }

        // Action to add an item to the cart
        [HttpPost]
        public IActionResult AddToCart(string image, string productName, decimal price)
        {
            // Check if the item is already in the cart
            var existingItem = CartItems.Find(item => item.ProductName == productName);

            if (existingItem != null)
            {
                // Update quantity if item already exists
                existingItem.Quantity++;
            }
            else
            {
                // Add new item to the cart
                var newItem = new Cart
                {
                    CartId = CartItems.Count + 1, // Use a better ID generation in production
                    Image = image,
                    ProductName = productName,
                    Price = price,
                    Quantity = 1
                };
                CartItems.Add(newItem);
            }

            return RedirectToAction("Index"); // Redirect to the cart page
        }
    }
}
