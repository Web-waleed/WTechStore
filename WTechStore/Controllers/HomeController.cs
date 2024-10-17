using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WTechStore.Data;
using WTechStore.Models;
using WTechStore.Models.ViewModels;

namespace WTechStore.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db;

        public HomeController(AppDbContext db)
        {
           this.db = db;
        }

        public IActionResult Index()
        {
            CategoryProductDataModel model= new CategoryProductDataModel
            {
                categories = db.Categories.OrderByDescending(x => x.CategoryId).ToList(),
                products = db.products.OrderByDescending(x => x.ProductId).ToList()
            
            };
            return View(model);
        }
        
        public IActionResult ProductDetails(int id) 
        { 
           
            var data = db.products.Find(id);
            if (data == null) { return RedirectToAction("Index"); }
            return View(data);
        }
        public IActionResult Computer(int? id)
        {
            var products = db.products.ToList().Where(x=>x.CategoryId == id);
            return View(products);
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
