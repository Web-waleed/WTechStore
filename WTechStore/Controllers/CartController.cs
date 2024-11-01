using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json; 

using System;
using System.Collections.Generic;
using WTechStore.Models;
using Microsoft.EntityFrameworkCore;
using WTechStore.Models.ViewModels;
using WTechStore.Data;
using Microsoft.AspNetCore.Authorization;

namespace WTechStore.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context; 
        }
        private const string CartCookie = "Cart";

        public IActionResult Index()
        {
            var cartItems = GetCartItemsFromCookies();
            ViewBag.CartItemCount = GetCartItemCount();
            return View(cartItems);
        }

        
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
            return RedirectToAction("Index", "Cart"); 
        }

        public int GetCartItemCount()
        {
            var cartItems = GetCartItemsFromCookies();
            return cartItems.Sum(item => item.Quantity); // Summing up quantities of all items
        }
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

       
        [HttpPost]
        public IActionResult ClearCart()
        {
            Response.Cookies.Delete(CartCookie);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            
            var cartItems = GetCartItemsFromCookies(); 

            
            var itemToUpdate = cartItems.FirstOrDefault(item => item.ProductId == productId);

          
            if (itemToUpdate != null && quantity > 0)
            {
                itemToUpdate.Quantity = quantity;
                SaveCartItemsToCookies(cartItems); 
            }

            

            return RedirectToAction("Index");
        }
        public IActionResult Checkout()
        {
            
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult SubmitOrder(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create the order
                var order = new OrderProduct
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    PaymentMethod = model.PaymentMethod,
                    OrderDate = DateTime.Now
                };

                // Get cart items from cookies and add each as a product to the order
                var cartItems = GetCartItemsFromCookies();
                foreach (var item in cartItems)
                {
                    var productItem = new ProductItem
                    {
                        ProductName = item.ProductName,
                        Price = item.Price,
                        ImageUrl = item.ImageUrl,
                        Quantity = item.Quantity
                    };
                    order.Products.Add(productItem);
                }

                // Save order and products to the database
                _context.orderProducts.Add(order);
                _context.SaveChanges();

                TempData["ConfirmationMessage"] = "Thank you! Your order has been placed successfully.";
                return RedirectToAction("OrderConfirmation");
            }

            // If validation fails, return to the same view with the model
            return View(model);
        }
        [Authorize]
        public IActionResult OrderConfirmation()
        {
            return View();
        }
        
        private List<CartItem>? GetCartItemsFromCookies()
        {
            var cookie = Request.Cookies[CartCookie];
           
            return string.IsNullOrEmpty(cookie)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(cookie);
        }

        private void SaveCartItemsToCookies(List<CartItem> cartItems)
        {
            
            var cookieValue = JsonConvert.SerializeObject(cartItems);
            Response.Cookies.Append(CartCookie, cookieValue, new CookieOptions { MaxAge = TimeSpan.FromDays(30) });
        }
    }
}
