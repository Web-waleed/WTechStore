using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using WTechStore.Models;

namespace WTechStore.Controllers
{
    public class WishlistsController : Controller
    {
        private const string WishlistCookie = "Wishlist";
        public IActionResult Index()
        {
            var wishlistItems = GetWishlistItemsFromCookies();
            ViewBag.WishlistCount = wishlistItems.Count;
            return View(wishlistItems); 
        }
        [HttpPost]
        public IActionResult AddToWishlist(int productId, string productName, decimal price, string imageUrl)
        {
            var wishlistItems = GetWishlistItemsFromCookies();
            var existingItem = wishlistItems.Find(item => item.Id == productId);

            if (existingItem == null)
            {
                wishlistItems.Add(new WishlistItem
                {
                    Id = productId,
                    ProductName = productName,
                    Price = price,
                    ImageUrl = imageUrl
                });
            }

            SaveWishlistItemsToCookies(wishlistItems);
            return RedirectToAction("Index");
        }
       
        [HttpPost]
        public IActionResult RemoveFromWishlist(int productId)
        {
            var wishlistItems = GetWishlistItemsFromCookies();
            var itemToRemove = wishlistItems.Find(item => item.Id == productId);

            if (itemToRemove != null)
            {
                wishlistItems.Remove(itemToRemove);
                SaveWishlistItemsToCookies(wishlistItems);
            }

            return RedirectToAction("Index");
        }

        private List<WishlistItem>? GetWishlistItemsFromCookies()
        {
            var cookie = Request.Cookies[WishlistCookie];
            return string.IsNullOrEmpty(cookie)
                ? new List<WishlistItem>()
                : JsonConvert.DeserializeObject<List<WishlistItem>>(cookie);
        }

        private void SaveWishlistItemsToCookies(List<WishlistItem> wishlistItems)
        {
            var cookieValue = JsonConvert.SerializeObject(wishlistItems);
            Response.Cookies.Append(WishlistCookie, cookieValue, new CookieOptions { MaxAge = TimeSpan.FromDays(30) });
        }
    }
}
