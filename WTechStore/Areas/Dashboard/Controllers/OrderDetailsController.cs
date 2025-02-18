using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WTechStore.Data;
using WTechStore.Models.ViewModels;

namespace WTechStore.Areas.Dashboard.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Dashboard")]
    public class OrderDetailsController : Controller
    {
        private readonly AppDbContext _context;

        public OrderDetailsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Orders()
        {
            var orders = _context.orderProducts
                .Include(o => o.Products)
                .Select(o => new OrderDetailViewModel
                {
                    Id = o.Id, 
                    FullName = o.FullName,
                    Email = o.Email,
                    PhoneNumber = o.PhoneNumber,
                    Address = o.Address,
                    OrderDate = o.OrderDate,
                    Products = o.Products.Select(p => new OrderDetailViewModel.Product
                    {
                        Id = p.Id,  // Assuming Id is the primary key for the Product
                        ProductName = p.ProductName,
                        Price = p.Price,
                        Quantity = p.Quantity
                    }).ToList()
                }).ToList();

            return View(orders);
        }

        [HttpPost]
        public IActionResult DeleteOrder(int orderId)
        {
            var order = _context.orderProducts
                .Include(o => o.Products) // Include related products to ensure they are loaded
                .FirstOrDefault(o => o.Id == orderId);

            if (order != null)
            {
              
                _context.orderProducts.Remove(order);
                _context.SaveChanges();
            }

            return RedirectToAction("Orders"); 
        }
    }
}
