using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WTechStore.Data;

namespace WTechStore.Areas.Dashboard.Controllers
{
    [Authorize(Roles ="admin")]
    public class HomeController : Controller
    {
        private AppDbContext context;
        public HomeController(AppDbContext context)
        {
            this.context = context;
        }
        [Area("Dashboard")]
        public IActionResult Index()
        {
            ViewBag.TotalProducts=context.products.Count();
            ViewBag.TotalCate=context.Categories.Count();
            return View();
        }
        public IActionResult Dashboard()
        {
            var contacts = context.contacts.ToList(); 
            return View(contacts); 
        }
    }
}
