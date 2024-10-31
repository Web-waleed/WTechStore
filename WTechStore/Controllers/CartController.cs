using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json; // For JsonSerializer

using System;
using System.Collections.Generic;
using WTechStore.Models;

namespace WTechStore.Controllers
{
    public class CartController : Controller
    {
        private const string CartCookie = "Cart";

        public IActionResult Index()
        {
            var cartItems = GetCartItemsFromCookies();
            return View(cartItems);
        }

        // Add product to cart
        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, decimal price, string imageUrl)
        {
            var cartItems = GetCartItemsFromCookies();
            var existingItem = cartItems.Find(item => item.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cartItems.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = productName,
                    Price = price,
                    ImageUrl = imageUrl,
                    Quantity = 1
                });
            }

            SaveCartItemsToCookies(cartItems);
            return RedirectToAction("Index");
        }

        // Remove item from cart
        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cartItems = GetCartItemsFromCookies();
            var itemToRemove = cartItems.Find(item => item.ProductId == productId);

            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
                SaveCartItemsToCookies(cartItems);
            }

            return RedirectToAction("Index");
        }

        // Clear cart
        [HttpPost]
        public IActionResult ClearCart()
        {
            Response.Cookies.Delete(CartCookie);
            return RedirectToAction("Index");
        }

        // Get cart items from cookies
        private List<CartItem>? GetCartItemsFromCookies()
        {
            var cookie = Request.Cookies[CartCookie];
            // Deserialize using Newtonsoft.Json
            return string.IsNullOrEmpty(cookie)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(cookie);
        }

        // Save cart items to cookies
        private void SaveCartItemsToCookies(List<CartItem> cartItems)
        {
            // Serialize using Newtonsoft.Json
            var cookieValue = JsonConvert.SerializeObject(cartItems);
            Response.Cookies.Append(CartCookie, cookieValue, new CookieOptions { MaxAge = TimeSpan.FromDays(30) });
        }
    }
}
