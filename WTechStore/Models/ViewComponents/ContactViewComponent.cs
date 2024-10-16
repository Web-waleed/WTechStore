using Microsoft.AspNetCore.Mvc;
using WTechStore.Data;

namespace WTechStore.Models.ViewComponents
{
    public class ContactViewComponent : ViewComponent
    {
        private AppDbContext db;
        public ContactViewComponent(AppDbContext db)
        {
            this.db = db;
        }
        public IViewComponentResult Invoke()
        {
            return View(db.contacts.ToList());
        }
    }
}
