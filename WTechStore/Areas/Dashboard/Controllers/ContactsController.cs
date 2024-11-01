using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WTechStore.Data;
using WTechStore.Models;

[Authorize]
[Area("Dashboard")]
public class ContactsController : Controller
{
    private readonly AppDbContext _context;

    public ContactsController(AppDbContext context)
    {
        _context = context;
    }

    
    [HttpPost]
    public async Task<IActionResult> Index(Contact contact)
    {
        if (ModelState.IsValid)  
        {
            _context.contacts.Add(contact);  
            await _context.SaveChangesAsync();  
            return RedirectToAction(nameof(Success));  
        }

        return View(contact);  
    }

   
    public async Task<IActionResult> Messages()
    {
        var contacts = await _context.contacts.ToListAsync();  
        return View(contacts); 
    }

  
    public async Task<IActionResult> Delete(int id)
    {
        var contact = await _context.contacts.FindAsync(id); 
        if (contact == null)
        {
            return NotFound();  
        }

        _context.contacts.Remove(contact);  
        await _context.SaveChangesAsync();  

        return RedirectToAction(nameof(Messages));  
    }

    
    public IActionResult Success()
    {
        return View();  
    }
}
