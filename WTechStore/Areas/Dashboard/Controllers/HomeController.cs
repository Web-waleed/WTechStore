using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WTechStore.Data;
using WTechStore.Models.ViewModels;

namespace WTechStore.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private AppDbContext context;
        private UserManager<ApplicationUser> userManager;
        public HomeController(AppDbContext context , UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
   
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalUsers = await userManager.Users.CountAsync();
            ViewBag.TotalProducts= await context.products.CountAsync();
            ViewBag.TotalCate= await context.Categories.CountAsync();
            
            return View();
        }
      
    }
}
