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
    public class DiscountCodesController : Controller
    {
        private readonly CatalogDBContext _context;

        public DiscountCodesController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: DiscountCodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiscountCodes.ToListAsync());
        }

        // GET: DiscountCodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountCodes = await _context.DiscountCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discountCodes == null)
            {
                return NotFound();
            }

            return View(discountCodes);
        }

        // GET: DiscountCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiscountCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Discount,CampaignStart,CampaignEnd,Timestamp")] DiscountCodes discountCodes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discountCodes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discountCodes);
        }

        // GET: DiscountCodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountCodes = await _context.DiscountCodes.FindAsync(id);
            if (discountCodes == null)
            {
                return NotFound();
            }
            return View(discountCodes);
        }

        // POST: DiscountCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Discount,CampaignStart,CampaignEnd,Timestamp")] DiscountCodes discountCodes)
        {
            if (id != discountCodes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discountCodes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountCodesExists(discountCodes.Id))
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
            return View(discountCodes);
        }

        // GET: DiscountCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountCodes = await _context.DiscountCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discountCodes == null)
            {
                return NotFound();
            }

            return View(discountCodes);
        }

        // POST: DiscountCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discountCodes = await _context.DiscountCodes.FindAsync(id);
            _context.DiscountCodes.Remove(discountCodes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountCodesExists(int id)
        {
            return _context.DiscountCodes.Any(e => e.Id == id);
        }
    }
}
