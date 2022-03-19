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
    public class OrderProductsController : Controller
    {
        private readonly CatalogDBContext _context;

        public OrderProductsController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: OrderProducts
        public async Task<IActionResult> Index()
        {
            var catalogDBContext = _context.OrderProducts.Include(o => o.Order);
            return View(await catalogDBContext.ToListAsync());
        }

        // GET: OrderProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProducts = await _context.OrderProducts
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderProducts == null)
            {
                return NotFound();
            }

            return View(orderProducts);
        }

        // GET: OrderProducts/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "CustomerAdress");
            return View();
        }

        // POST: OrderProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Price,Amount,ProductSize,ProductColor,ProductNumber,OrderId")] OrderProducts orderProducts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderProducts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "CustomerAdress", orderProducts.OrderId);
            return View(orderProducts);
        }

        // GET: OrderProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProducts = await _context.OrderProducts.FindAsync(id);
            if (orderProducts == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "CustomerAdress", orderProducts.OrderId);
            return View(orderProducts);
        }

        // POST: OrderProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Price,Amount,ProductSize,ProductColor,ProductNumber,OrderId")] OrderProducts orderProducts)
        {
            if (id != orderProducts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderProducts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderProductsExists(orderProducts.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "CustomerAdress", orderProducts.OrderId);
            return View(orderProducts);
        }

        // GET: OrderProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProducts = await _context.OrderProducts
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderProducts == null)
            {
                return NotFound();
            }

            return View(orderProducts);
        }

        // POST: OrderProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderProducts = await _context.OrderProducts.FindAsync(id);
            _context.OrderProducts.Remove(orderProducts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderProductsExists(int id)
        {
            return _context.OrderProducts.Any(e => e.Id == id);
        }
    }
}
