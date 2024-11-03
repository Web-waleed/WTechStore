using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WTechStore.Data;
using WTechStore.Models;
using WTechStore.Models.ViewModels;

namespace WTechStore.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db;
        private const string CartCookie = "Cart";

        public HomeController(AppDbContext db)
        {
           this.db = db;
        }

        public IActionResult Index()
        {
            ViewBag.CartItemCount = GetCartItemCount();
            ViewBag.WishlistCount = GetWishlistCount();
            CategoryProductDataModel model= new CategoryProductDataModel
            {
                categories = db.Categories.OrderByDescending(x => x.CategoryId).ToList(),
                products = db.products.OrderByDescending(x => x.ProductId).ToList()
             
            };
            return View(model);
        }
        
        public IActionResult ProductDetails(int id) 
        {
            ViewBag.CartItemCount = GetCartItemCount();
            ViewBag.WishlistCount = GetWishlistCount();
            var data = db.products.Find(id);
            if (data == null) { return RedirectToAction("Index"); }
            return View(data);
        }
        public IActionResult Computer(int? id)
        {
            ViewBag.CartItemCount = GetCartItemCount();
            ViewBag.WishlistCount = GetWishlistCount();
            var products = db.products.ToList().Where(x=>x.CategoryId == id);
            return View(products);
        }
        public int GetCartItemCount()
        {
            var cookie = Request.Cookies["Cart"];
            if (string.IsNullOrEmpty(cookie))
            {
                return 0; 
            }

            var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cookie);
            return cartItems.Sum(item => item.Quantity);
        }
        public int GetWishlistCount()
        {
            var cookie = Request.Cookies["Wishlist"];
            if (string.IsNullOrEmpty(cookie))
            {
                return 0; 
            }

            var wishlistItems = JsonConvert.DeserializeObject<List<WishlistItem>>(cookie);
            return wishlistItems.Count;
        }
        public IActionResult Cart() {
             
            return View();
        }
       
        public IActionResult WishList() { 
        return View();  
        }
        public IActionResult AboutUs() {
            return View();
        }
        public IActionResult Blog() {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }


        
    }
}
