using Microsoft.AspNetCore.Mvc;
using WTechStore.Data;

namespace WTechStore.Models.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
        private AppDbContext db;
        public SliderViewComponent(AppDbContext db)
        {
            this.db = db;
        }
        public IViewComponentResult Invoke()
        {
            return View(db.Sliders.ToList());
        }
    }
}
