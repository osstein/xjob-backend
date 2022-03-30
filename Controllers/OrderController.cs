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
    public class OrderController : Controller
    {
        private readonly CatalogDBContext _context;

        public OrderController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.ONSortParm = sortOrder == "ordernumber" ? "ordernumber_desc" : "ordernumber";
            ViewBag.RNSortParm = sortOrder == "receiptnumber" ? "receiptnumber_desc" : "receiptnumber";
            ViewBag.MAILSortParm = sortOrder == "mail" ? "mail_desc" : "mail";
            ViewBag.PAYMENTSortParm = sortOrder == "payment" ? "payment_desc" : "payment";
            var objects = from order in _context.Order
                          select order;
            switch (sortOrder)
            {
                case "Id_desc":
                    objects = objects.OrderByDescending(s => s.Id);
                    break;
                case "Date":
                    objects = objects.OrderBy(s => s.Timestamp);
                    break;
                case "date_desc":
                    objects = objects.OrderByDescending(s => s.Timestamp);
                    break;
                case "ordernumber":
                    objects = objects.OrderBy(s => s.OrderNumber);
                    break;
                case "ordernumber_desc":
                    objects = objects.OrderByDescending(s => s.OrderNumber);
                    break;
                case "receiptnumber":
                    objects = objects.OrderBy(s => s.ReceiptNumber);
                    break;
                case "receiptnumber_desc":
                    objects = objects.OrderByDescending(s => s.ReceiptNumber);
                    break;
                case "mail":
                    objects = objects.OrderBy(s => s.CustomerMail);
                    break;
                case "mail_desc":
                    objects = objects.OrderByDescending(s => s.CustomerMail);
                    break;
            
                case "payment":
                    objects = objects.OrderBy(s => s.PaymentMethod);
                    break;
                case "payment_desc":
                    objects = objects.OrderByDescending(s => s.PaymentMethod);
                    break;
                default:
                    objects = objects.OrderBy(s => s.Id);
                    break;
            }
            return View(await objects.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerFirstName,CustomerLastName,CustomerMail,CustomerPhone,CustomerAdress,CustomerZip,CustomerCity,PriceTotal,VatTotal,DiscountTotal,DiscountCode,OrderNumber,ReceiptNumber,PaymentMethod,Timestamp,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Order.Add(order);
                await _context.SaveChangesAsync();
                //Save instance of order
                order.OrderNumber = "OL-" + DateTime.Now.ToString("yyyyMMddssfff");
                order.ReceiptNumber = "RN-" + new Random().Next(11) + "-" + DateTime.Now.ToString("yyyyMMddssfff");
                _context.Order.Update(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerFirstName,CustomerLastName,CustomerMail,CustomerPhone,CustomerAdress,CustomerZip,CustomerCity,PriceTotal,VatTotal,DiscountTotal,DiscountCode,OrderNumber,ReceiptNumber,PaymentMethod,Timestamp,Status")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderProducts = from OrderProducts in _context.OrderProducts select OrderProducts; ;

            foreach (var item in orderProducts)
            {
                if (item.OrderId == id)
                {
                    _context.OrderProducts.Remove(item);
                }
            }

            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
