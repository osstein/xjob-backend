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
    public class CatalogCategoriesController : Controller
    {
        private readonly CatalogDBContext _context;

        public CatalogCategoriesController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: CatalogCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.CatalogCategories.ToListAsync());
        }

        // GET: CatalogCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogCategories = await _context.CatalogCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogCategories == null)
            {
                return NotFound();
            }

            return View(catalogCategories);
        }

        // GET: CatalogCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CatalogCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category")] CatalogCategories catalogCategories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalogCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catalogCategories);
        }

        // GET: CatalogCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogCategories = await _context.CatalogCategories.FindAsync(id);
            if (catalogCategories == null)
            {
                return NotFound();
            }
            return View(catalogCategories);
        }

        // POST: CatalogCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category")] CatalogCategories catalogCategories)
        {
            if (id != catalogCategories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogCategoriesExists(catalogCategories.Id))
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
            return View(catalogCategories);
        }

        // GET: CatalogCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogCategories = await _context.CatalogCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogCategories == null)
            {
                return NotFound();
            }

            return View(catalogCategories);
        }

        // POST: CatalogCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalogCategories = await _context.CatalogCategories.FindAsync(id);
            _context.CatalogCategories.Remove(catalogCategories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogCategoriesExists(int id)
        {
            return _context.CatalogCategories.Any(e => e.Id == id);
        }
    }
}
