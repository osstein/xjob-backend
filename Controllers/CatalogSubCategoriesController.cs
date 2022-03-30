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

namespace backend.Controllers
{
    public class CatalogSubCategoriesController : Controller
    {
        private readonly CatalogDBContext _context;

        public CatalogSubCategoriesController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: CatalogSubCategories
        public async Task<IActionResult> Index()
        {
            var catalogDBContext = _context.CatalogSubCategories.Include(c => c.CatalogCategories);
            return View(await catalogDBContext.OrderBy(s => s.Category).ToListAsync());
        }

        // GET: CatalogSubCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogSubCategories = await _context.CatalogSubCategories
                .Include(c => c.CatalogCategories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogSubCategories == null)
            {
                return NotFound();
            }

            return View(catalogSubCategories);
        }

        // GET: CatalogSubCategories/Create
        public IActionResult Create()
        {
            ViewData["CatalogCategoriesId"] = new SelectList(_context.CatalogCategories, "Id", "Category");
            return View();
        }

        // POST: CatalogSubCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category,CatalogCategoriesId")] CatalogSubCategories catalogSubCategories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalogSubCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatalogCategoriesId"] = new SelectList(_context.CatalogCategories, "Id", "Category", catalogSubCategories.CatalogCategoriesId);
            return View(catalogSubCategories);
        }

        // GET: CatalogSubCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogSubCategories = await _context.CatalogSubCategories.FindAsync(id);
            if (catalogSubCategories == null)
            {
                return NotFound();
            }
            ViewData["CatalogCategoriesId"] = new SelectList(_context.CatalogCategories, "Id", "Category", catalogSubCategories.CatalogCategoriesId);
            return View(catalogSubCategories);
        }

        // POST: CatalogSubCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,CatalogCategoriesId")] CatalogSubCategories catalogSubCategories)
        {
            if (id != catalogSubCategories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogSubCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogSubCategoriesExists(catalogSubCategories.Id))
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
            ViewData["CatalogCategoriesId"] = new SelectList(_context.CatalogCategories, "Id", "Category", catalogSubCategories.CatalogCategoriesId);
            return View(catalogSubCategories);
        }

        // GET: CatalogSubCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogSubCategories = await _context.CatalogSubCategories
                .Include(c => c.CatalogCategories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogSubCategories == null)
            {
                return NotFound();
            }

            return View(catalogSubCategories);
        }

        // POST: CatalogSubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalogSubCategories = await _context.CatalogSubCategories.FindAsync(id);
            _context.CatalogSubCategories.Remove(catalogSubCategories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogSubCategoriesExists(int id)
        {
            return _context.CatalogSubCategories.Any(e => e.Id == id);
        }
    }
}
