using Microsoft.AspNetCore.Mvc;
using WTechStore.Data;

namespace WTechStore.Models.ViewComponents
{
    public class FeedbackViewComponent : ViewComponent
    {
        private AppDbContext db;
        public FeedbackViewComponent(AppDbContext db)
        {
            this.db = db;
        }
        public IViewComponentResult Invoke()
        {
            return View(db.feedbacks.ToList());
        }
    }
}
