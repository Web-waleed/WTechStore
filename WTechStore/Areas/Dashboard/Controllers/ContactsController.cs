using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WTechStore.Data;
using WTechStore.Models;

[Authorize(Roles = "admin")]
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
            _context.contacts.Add(contact);  // Make sure this DbSet is named "contacts"
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));  // Redirect to a Success page
        }

        return View(contact);  // Return the form with validation errors if invalid
    }


    public async Task<IActionResult> Messages()
    {
        var contacts = await _context.contacts.ToListAsync();  // Fetches all contacts.
        return View(contacts);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var contact = await _context.contacts.FindAsync(id);
        if (contact == null)
        {
            return NotFound();  // Return NotFound if contact doesn't exist.
        }

        _context.contacts.Remove(contact);
        await _context.SaveChangesAsync();

        return RedirectToAction("Messages");  // Redirect back to Messages after deletion.
    }

    
}
