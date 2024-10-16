using Microsoft.AspNetCore.Mvc;
using WTechStore.Data;

namespace WTechStore.Models.ViewComponents
{
    public class advertisementViewComponent : ViewComponent
    {
        private AppDbContext db;
        public advertisementViewComponent(AppDbContext db)
        {
            this.db = db;
        }
        public IViewComponentResult Invoke()
        {
            return View(db.advertisements.ToList());
        }
    }
}
