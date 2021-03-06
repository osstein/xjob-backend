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
    public class ProductImagesController : Controller
    {
        private readonly CatalogDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductImagesController(CatalogDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: ProductImages
        [Authorize]
        public async Task<IActionResult> Index(string Id)
        {
            if (Id != null)
            {
                var catalogDBContext = _context.ProductImages.Include(p => p.Product).Where(s => s.ProductId == Convert.ToInt32(Id));
                ViewData["count"] = catalogDBContext.Count();
                return View(await catalogDBContext.ToListAsync());
            }
            else
            {
                var catalogDBContext = _context.ProductImages.Include(p => p.Product);
                ViewData["count"] = catalogDBContext.Count();
                return View(await catalogDBContext.ToListAsync());
            }
        }

        // GET: ProductImages/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImages = await _context.ProductImages
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImages == null)
            {
                return NotFound();
            }

            return View(productImages);
        }

        // GET: ProductImages/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
            return View();
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,ImagePath,ImageFile,ImageAlt,ProductId")] ProductImages productImages)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _hostEnvironment.WebRootPath; // wwwroot path
                if (productImages.ImageFile != null)
                {
                    // Adjust image filename
                    string filename = Path.GetFileNameWithoutExtension(productImages.ImageFile.FileName); // Filename
                    string extention = Path.GetExtension(productImages.ImageFile.FileName); // extention
                    string name = filename + DateTime.Now.ToString("yyyyMMddssfff") + extention;
                    string url = Path.Combine("/images/" + name);
                    productImages.ImagePath = url;
                    //Store file
                    using (var FileStream = new FileStream(wwwRoot + "/images/" + name, FileMode.Create))
                    {
                        await productImages.ImageFile.CopyToAsync(FileStream);
                    }
                    //editImages(name);
                }
                _context.Add(productImages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productImages.ProductId);
            return View(productImages);
        }

        // GET: ProductImages/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImages = await _context.ProductImages.FindAsync(id);
            if (productImages == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productImages.ProductId);
            return View(productImages);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImagePath,ImageFile,ImageAlt,ProductId")] ProductImages productImages)
        {
            if (id != productImages.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    productImages.ImagePath = productImages.ImagePath;
                    _context.Update(productImages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductImagesExists(productImages.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productImages.ProductId);
            return View(productImages);
        }

        // GET: ProductImages/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImages = await _context.ProductImages
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImages == null)
            {
                return NotFound();
            }

            return View(productImages);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productImages = await _context.ProductImages.FindAsync(id);
            _context.ProductImages.Remove(productImages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductImagesExists(int id)
        {
            return _context.ProductImages.Any(e => e.Id == id);
        }
    }
}
