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
    public class ProductTypeController : Controller
    {
        private readonly CatalogDBContext _context;

        public ProductTypeController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: ProductType
        public async Task<IActionResult> Index(string Id, string sortOrder)
        {
            if (Id != null)
            {
                ViewData["Sizes"] = await _context.ProductSize.ToListAsync();
                ViewData["Colors"] = await _context.ProductColor.ToListAsync();
                var catalogDBContext =  _context.ProductType.Include(p => p.Product).Where(s => s.ProductId == Convert.ToInt32(Id));

                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

                switch (sortOrder)
                {
                    case "name_desc":
                        catalogDBContext = catalogDBContext.OrderByDescending(s => s.ProductId);
                        break;
                    case "Date":
                        catalogDBContext = catalogDBContext.OrderBy(s => s.Amount);
                        break;
                    case "date_desc":
                        catalogDBContext = catalogDBContext.OrderByDescending(s => s.Amount);
                        break;
                    default:
                        catalogDBContext = catalogDBContext.OrderBy(s => s.ProductId);
                        break;
                }


                return View(await catalogDBContext.ToListAsync());
            }
            else
            {

                ViewData["Sizes"] = await _context.ProductSize.ToListAsync();
                ViewData["Colors"] = await _context.ProductColor.ToListAsync();
                var catalogDBContext = _context.ProductType.Include(p => p.Product).Where(s => s.ProductId != null);

                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.AmountSortParm = sortOrder == "Amount" ? "amount_desc" : "Amount";
                ViewBag.ColorSortParm = sortOrder == "Color" ? "color_desc" : "Color";
                ViewBag.SizeSortParm = sortOrder == "Size" ? "size_desc" : "Size";

                switch (sortOrder)
                {
                    case "name_desc":
                        catalogDBContext = catalogDBContext.OrderByDescending(s => s.ProductId);
                        break;
                    case "Amount":
                        catalogDBContext = catalogDBContext.OrderBy(s => s.Amount);
                        break;
                    case "amount_desc":
                        catalogDBContext = catalogDBContext.OrderByDescending(s => s.Amount);
                        break;
                    case "Color":
                        catalogDBContext = catalogDBContext.OrderBy(s => s.ProductColorId);
                        break;
                    case "color_desc":
                        catalogDBContext = catalogDBContext.OrderByDescending(s => s.ProductColorId);
                        break;
                        case "Size":
                        catalogDBContext = catalogDBContext.OrderBy(s => s.ProductSizeId);
                        break;
                    case "size_desc":
                        catalogDBContext = catalogDBContext.OrderByDescending(s => s.ProductSizeId);
                        break;
                    default:
                        catalogDBContext = catalogDBContext.OrderBy(s => s.ProductId);
                        break;
                }

                return View(await catalogDBContext.ToListAsync());
            }
        }

        // GET: ProductType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType
                .Include(p => p.Product)
                .Include(p => p.ProductColor)
                .Include(p => p.ProductSize)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: ProductType/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
            ViewData["ProductColorId"] = new SelectList(_context.ProductColor, "Id", "Color");
            ViewData["ProductSizeId"] = new SelectList(_context.ProductSize, "Id", "Size");
            return View();
        }

        // POST: ProductType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,ProductId,ProductColorId,ProductSizeId")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productType.ProductId);
            ViewData["ProductColorId"] = new SelectList(_context.ProductColor, "Id", "Color", productType.ProductColorId);
            ViewData["ProductSizeId"] = new SelectList(_context.ProductSize, "Id", "Size", productType.ProductSizeId);
            return View(productType);
        }

        // GET: ProductType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productType.ProductId);
            ViewData["ProductColorId"] = new SelectList(_context.ProductColor, "Id", "Color", productType.ProductColorId);
            ViewData["ProductSizeId"] = new SelectList(_context.ProductSize, "Id", "Size", productType.ProductSizeId);
            return View(productType);
        }

        // POST: ProductType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,ProductId,ProductColorId,ProductSizeId")] ProductType productType)
        {
            if (id != productType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(productType.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productType.ProductId);
            ViewData["ProductColorId"] = new SelectList(_context.ProductColor, "Id", "Color", productType.ProductColorId);
            ViewData["ProductSizeId"] = new SelectList(_context.ProductSize, "Id", "Size", productType.ProductSizeId);
            return View(productType);
        }

        // GET: ProductType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType
                .Include(p => p.Product)
                .Include(p => p.ProductColor)
                .Include(p => p.ProductSize)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: ProductType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productType = await _context.ProductType.FindAsync(id);
            _context.ProductType.Remove(productType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTypeExists(int id)
        {
            return _context.ProductType.Any(e => e.Id == id);
        }
    }
}
