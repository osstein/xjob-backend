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
    public class ProductController : Controller
    {
        private readonly CatalogDBContext _context;

        public ProductController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: Product
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            var sub = from CatalogSubCategories in _context.CatalogSubCategories select CatalogSubCategories;
            var main = from CatalogCategories in _context.CatalogCategories select CatalogCategories;

            foreach (var low in sub)
            {
                foreach (var top in main)
                {
                    if (top.Id == low.CatalogCategoriesId)
                    {
                        low.Category = low.Category + " (" + top.Category + ")";
                    }
                }
            }
            ViewData["Categories"] = await sub.ToListAsync();
            var Images = from ProductImages in _context.ProductImages select ProductImages;
            ViewData["Images"] = await Images.ToListAsync();


            var catalogDBContext = from product in _context.Product.Include(p => p.CatalogSubCategories) select product;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.PNRSortParm = sortOrder == "productnr" ? "productnr_desc" : "productnr";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
            if (!String.IsNullOrEmpty(searchString))
            {
                catalogDBContext = catalogDBContext.Where(s => s.ProductNumber.ToLower().Contains(searchString.ToLower()) || s.Name.ToLower().Contains(searchString.ToLower()));
            }
            ViewData["count"] = catalogDBContext.Count();
            switch (sortOrder)
            {
                case "name_desc":
                    catalogDBContext = catalogDBContext.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    catalogDBContext = catalogDBContext.OrderBy(s => s.Timestamp);
                    break;
                case "date_desc":
                    catalogDBContext = catalogDBContext.OrderByDescending(s => s.Timestamp);
                    break;
                case "productnr":
                    catalogDBContext = catalogDBContext.OrderBy(s => s.ProductNumber);
                    break;
                case "productnr_desc":
                    catalogDBContext = catalogDBContext.OrderByDescending(s => s.Price);
                    break;
                /* case "price":
                    catalogDBContext = catalogDBContext.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    catalogDBContext = catalogDBContext.OrderByDescending(s => s.ProductNumber);
                    break; */
                default:
                    catalogDBContext = catalogDBContext.OrderBy(s => s.Name);
                    break;
            }



            return View(await catalogDBContext.ToListAsync());
        }

        // GET: Product/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.CatalogSubCategories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var sub = from CatalogSubCategories in _context.CatalogSubCategories select CatalogSubCategories;
            var main = from CatalogCategories in _context.CatalogCategories select CatalogCategories; ;

            foreach (var low in sub)
            {
                foreach (var top in main)
                {
                    if (top.Id == low.CatalogCategoriesId)
                    {
                        low.Category = low.Category + " (" + top.Category + ")";
                    }
                }
            }
            ViewData["Categories"] = await sub.ToListAsync();

            return View(product);
        }

        // GET: Product/Create
        [Authorize]
        public IActionResult Create()
        {

            var sub = from CatalogSubCategories in _context.CatalogSubCategories select CatalogSubCategories;
            var main = from CatalogCategories in _context.CatalogCategories select CatalogCategories; ;

            foreach (var low in sub)
            {
                foreach (var top in main)
                {
                    if (top.Id == low.CatalogCategoriesId)
                    {
                        low.Category = low.Category + " (" + top.Category + ")";
                    }
                }
            }

            ViewData["CatalogSubCategoriesId"] = new SelectList(sub, "Id", "Category");

            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Vat,Discount,Description,ProductNumber,CatalogSubCategoriesId,Timestamp")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Vat > 25)
                {
                    product.Vat = 25;
                }
                if (product.Discount > 100)
                {
                    product.Discount = 100;
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatalogSubCategoriesId"] = new SelectList(_context.CatalogSubCategories, "Id", "Category", product.CatalogSubCategoriesId);
            return View(product);
        }

        // GET: Product/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var sub = from CatalogSubCategories in _context.CatalogSubCategories select CatalogSubCategories;
            var main = from CatalogCategories in _context.CatalogCategories select CatalogCategories; ;

            foreach (var low in sub)
            {
                foreach (var top in main)
                {
                    if (top.Id == low.CatalogCategoriesId)
                    {
                        low.Category = low.Category + " (" + top.Category + ")";
                    }
                }
            }

            ViewData["CatalogSubCategoriesId"] = new SelectList(sub, "Id", "Category");
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Vat,Discount,Description,ProductNumber,CatalogSubCategoriesId,Timestamp")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.Vat > 25)
                    {
                        product.Vat = 25;
                    }
                    if (product.Discount > 100)
                    {
                        product.Discount = 100;
                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CatalogSubCategoriesId"] = new SelectList(_context.CatalogSubCategories, "Id", "Category", product.CatalogSubCategoriesId);
            return View(product);
        }

        // GET: Product/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.CatalogSubCategories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var sub = from CatalogSubCategories in _context.CatalogSubCategories select CatalogSubCategories;
            var main = from CatalogCategories in _context.CatalogCategories select CatalogCategories; ;

            foreach (var low in sub)
            {
                foreach (var top in main)
                {
                    if (top.Id == low.CatalogCategoriesId)
                    {
                        low.Category = low.Category + " (" + top.Category + ")";
                    }
                }
            }
            ViewData["Categories"] = await sub.ToListAsync();

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
