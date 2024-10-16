using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WTechStore.Data;
using WTechStore.Models;

namespace WTechStore.Areas.Dashboard.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Dashboard")]
    public class advertisementsController : Controller
    {
        private readonly AppDbContext _context;

        public advertisementsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/advertisements
        public async Task<IActionResult> Index()
        {
            return View(await _context.advertisements.ToListAsync());
        }

        // GET: Dashboard/advertisements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.advertisements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Dashboard/advertisements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/advertisements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                if (advertisement.ImageFile != null && advertisement.ImageFile.Length > 0)
                {
                    // Define the path to save the image
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var fileName = Path.GetFileName(advertisement.ImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Ensure the uploads folder exists
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await advertisement.ImageFile.CopyToAsync(fileStream);
                    }

                    // Set the image path in the model
                    advertisement.adsimg = $"/images/{fileName}";
                }

                // Save the product to the database
                _context.advertisements.Add(advertisement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate the CategoryId dropdown if validation fails
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", advertisement.Id);
            return View(advertisement);
        }



        // GET: Dashboard/advertisements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.advertisements.FindAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }
            return View(advertisement);
        }

        // POST: Dashboard/advertisements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, advertisement ads)
        {
            if (id != ads.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing advertisement from the database
                    var existingAds = await _context.advertisements.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
                    if (existingAds == null)
                    {
                        return NotFound();
                    }

                    // Check if an image file has been uploaded
                    if (ads.ImageFile != null && ads.ImageFile.Length > 0)
                    {
                        // Define the path to save the image
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        var fileName = Path.GetFileName(ads.ImageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        // Ensure the uploads folder exists
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Save the file to the server
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await ads.ImageFile.CopyToAsync(fileStream);
                        }

                        // Update the image path in the model
                        ads.adsimg = $"/images/{fileName}";
                    }
                    else
                    {
                        // If no new file is uploaded, retain the existing image path
                        ads.adsimg = existingAds.adsimg;
                    }

                    // Update the advertisement in the database
                    _context.Entry(ads).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!advertisementExists(ads.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ads);
        }
        // GET: Dashboard/Sliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.advertisements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }
        // POST: Dashboard/advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisement = await _context.advertisements.FindAsync(id);
            if (advertisement != null)
            {
                _context.advertisements.Remove(advertisement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool advertisementExists(int id)
        {
            return _context.advertisements.Any(e => e.Id == id);
        }
    }
}
