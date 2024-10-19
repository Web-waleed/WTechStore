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

    // Handles form submission and adds contact message to the database
    [HttpPost]
    public async Task<IActionResult> Index(Contact contact)
    {
        if (ModelState.IsValid)  // Validate the form data
        {
            _context.contacts.Add(contact);  // Add the contact message to the database
            await _context.SaveChangesAsync();  // Save changes asynchronously
            return RedirectToAction(nameof(Success));  // Redirect to the Success page after submission
        }

        return View(contact);  // If invalid, return the form with validation errors
    }

    // Displays a list of all contact messages
    public async Task<IActionResult> Messages()
    {
        var contacts = await _context.contacts.ToListAsync();  // Fetch all contact messages from the database
        return View(contacts);  // Return the messages to the view
    }

    // Handles deleting a contact message by ID
    public async Task<IActionResult> Delete(int id)
    {
        var contact = await _context.contacts.FindAsync(id);  // Find the contact by ID
        if (contact == null)
        {
            return NotFound();  // If no contact found, return a 404
        }

        _context.contacts.Remove(contact);  // Remove the contact message
        await _context.SaveChangesAsync();  // Save changes asynchronously

        return RedirectToAction(nameof(Messages));  // Redirect back to the list of messages after deletion
    }

    // Shows a success page after form submission
    public IActionResult Success()
    {
        return View();  // Return the Success view after submission
    }
}
