#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    public class ProductPropertiesController : Controller
    {
        private readonly CatalogDBContext _context;

        public ProductPropertiesController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: ProductProperties
        [Authorize]
        public async Task<IActionResult> Index(string Id)
        {
            if (Id != null)
            {
                var catalogDBContext = _context.ProductProperties.Include(p => p.Product).Where(s => s.ProductId == Convert.ToInt32(Id));
                ViewData["count"] = catalogDBContext.Count();
                return View(await catalogDBContext.ToListAsync());
            }
            else
            {
                var catalogDBContext = _context.ProductProperties.Include(p => p.Product);
                ViewData["count"] = catalogDBContext.Count();
                return View(await catalogDBContext.ToListAsync());
            }
        }

        // GET: ProductProperties/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productProperties = await _context.ProductProperties
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productProperties == null)
            {
                return NotFound();
            }

            return View(productProperties);
        }

        // GET: ProductProperties/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
            return View();
        }

        // POST: ProductProperties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,Text,ProductId")] ProductProperties productProperties)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productProperties);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productProperties.ProductId);
            return View(productProperties);
        }

        // GET: ProductProperties/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productProperties = await _context.ProductProperties.FindAsync(id);
            if (productProperties == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Description", productProperties.ProductId);
            return View(productProperties);
        }

        // POST: ProductProperties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Text,ProductId")] ProductProperties productProperties)
        {
            if (id != productProperties.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productProperties);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPropertiesExists(productProperties.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Description", productProperties.ProductId);
            return View(productProperties);
        }

        // GET: ProductProperties/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productProperties = await _context.ProductProperties
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productProperties == null)
            {
                return NotFound();
            }

            return View(productProperties);
        }

        // POST: ProductProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productProperties = await _context.ProductProperties.FindAsync(id);
            _context.ProductProperties.Remove(productProperties);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPropertiesExists(int id)
        {
            return _context.ProductProperties.Any(e => e.Id == id);
        }
    }
}
