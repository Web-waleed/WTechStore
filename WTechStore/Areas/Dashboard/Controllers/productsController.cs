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
    public class productsController : Controller
    {
        private readonly AppDbContext _context;

        public productsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/products
        public async Task<IActionResult> Index(int? categoryId)
        {
            var productsQuery = _context.products.Include(p => p.Category).AsQueryable();
            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);
            }

            var appDbContext = _context.products.Include(p => p.Category);
            ViewBag.categories = await _context.Categories.ToListAsync();
            return View(await productsQuery.ToListAsync());
        }

        // GET: Dashboard/products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

 
        // GET: Dashboard/products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {
                    // Define the path to save the image
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var fileName = Path.GetFileName(product.ImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Ensure the uploads folder exists
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                    }

                    // Set the image path in the model
                    product.ProductIMg = $"/images/{fileName}";
                }

                // Save the product to the database
                _context.products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate the CategoryId dropdown if validation fails
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }



        // GET: Dashboard/products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Dashboard/products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing product from the database
                    var existingProduct = await _context.products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
                    if (existingProduct == null)
                    {
                        return NotFound();
                    }

                    // Check if an image file has been uploaded
                    if (product.ImageFile != null && product.ImageFile.Length > 0)
                    {
                        // Define the path to save the image
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        var fileName = Path.GetFileName(product.ImageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        // Ensure the uploads folder exists
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Save the new image file to the server
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await product.ImageFile.CopyToAsync(fileStream);
                        }

                        // Update the image path in the model
                        product.ProductIMg = $"/images/{fileName}";
                    }
                    else
                    {
                        // If no new file is uploaded, retain the existing image path
                        product.ProductIMg = existingProduct.ProductIMg;
                    }

                    // Update the product in the database
                    _context.Entry(product).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!productExists(product.ProductId))
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

            return View(product);
        }

       


        // GET: Dashboard/products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Dashboard/products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.products.FindAsync(id);
            if (product != null)
            {
                _context.products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool productExists(int id)
        {
            return _context.products.Any(e => e.ProductId == id);
        }
    }
}
