using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTechStore.Data;
using WTechStore.Models;
using WTechStore.Models.ViewModels;

namespace WTechStore.Areas.Dashboard.Controllers
{
    [Authorize]
    [Area("Dashboard")]

    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }
       
        [HttpPost]
        public IActionResult SubmitOrder( OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
              
                Order newOrder = new Order
                {
                    FullName = order.FullName,
                    Email = order.Email,
                    PhoneNumber = order.PhoneNumber,
                    Address = order.Address,
                    PaymentMethod = order.PaymentMethod,
                  
                    OrderItems = order.CartItems.Select(c => new OrderItem
                    {
                        OrderItemId = c.Quantity,
                        Quantity = c.Quantity,
                        Price = c.Price
                    }).ToList()
                };

                _context.Orders.Add(newOrder);
                _context.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Invalid input" });
        }
    }
}
