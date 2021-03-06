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
    public class OrderProductsController : Controller
    {
        private readonly CatalogDBContext _context;

        public OrderProductsController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: OrderProducts
       [Authorize] public async Task<IActionResult> Index(string Id)
        {
            var catalogDBContext = from OrderProducts in _context.OrderProducts.Include(o => o.Order).Where(o => o.OrderId != null) select OrderProducts;
            if (Id != null)
            {
                catalogDBContext = from OrderProducts in _context.OrderProducts.Include(o => o.Order).Where(o => o.OrderId == Convert.ToInt32(Id)) select OrderProducts;
            }
            ViewData["count"] = catalogDBContext.Count();
            return View(await catalogDBContext.ToListAsync());
        }

        // GET: OrderProducts/Details/5
      [Authorize]  public async Task<IActionResult> Details(int? id)
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
      [Authorize]  public IActionResult Create(int id, string color, string size, string productName, string productNumber, int productId)
        {
            ViewData["TypeId"] = id;
            ViewData["Color"] = color;
            ViewData["Size"] = size;
            ViewData["ProductId"] = productId;
            ViewData["ProductNumber"] = productNumber;
            ViewData["ProductName"] = productName;
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "OrderNumber");
            return View();
        }

        // POST: OrderProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
     [Authorize]   public async Task<IActionResult> Create([Bind("Id,ProductId,Price,Amount,ProductSize,ProductColor,ProductNumber,OrderId")] OrderProducts orderProducts)
        {
            if (ModelState.IsValid)
            {

                var singleProduct = await _context.Product.FindAsync(orderProducts.ProductId);
                orderProducts.ProductNumber = singleProduct.ProductNumber;

                _context.Add(orderProducts);
                await _context.SaveChangesAsync();
                var order = await _context.Order.FindAsync(orderProducts.OrderId);
                var products = from OrderProducts in _context.OrderProducts select OrderProducts;
                order.PriceTotal = 0;
                order.VatTotal = 0;
                order.DiscountTotal = 0;


                foreach (var item in products)
                {
                    if (item.OrderId == orderProducts.OrderId)
                    {
                        var mainProduct = await _context.Product.FindAsync(item.ProductId);
                        order.PriceTotal = order.PriceTotal + (mainProduct.Price * item.Amount * (100 - mainProduct.Discount) / 100);
                        order.VatTotal = order.VatTotal + ((mainProduct.Vat / 100) * (mainProduct.Price * (100 - mainProduct.Discount) / 100) * item.Amount);
                        orderProducts.Price = mainProduct.Price * item.Amount * (100 - mainProduct.Discount) / 100;
                    }
                }
                if (order.DiscountCode != null)
                {
                    var discountCodes = from DiscountCodes in _context.DiscountCodes select DiscountCodes;
                    foreach (var code in discountCodes)
                    {
                        if (code.Code == order.DiscountCode)
                        {
                            order.DiscountTotal = order.PriceTotal * (code.Discount / 100);
                        }
                    }
                }

                _context.Update(order);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "OrderNumber", orderProducts.OrderId);
            return View(orderProducts);
        }

     

        // GET: OrderProducts/Delete/5
      [Authorize]  public async Task<IActionResult> Delete(int? id)
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
      [Authorize]  public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderProducts = await _context.OrderProducts.FindAsync(id);
            _context.OrderProducts.Remove(orderProducts);
            await _context.SaveChangesAsync();
            var order = await _context.Order.FindAsync(orderProducts.OrderId);
            var products = from OrderProducts in _context.OrderProducts select OrderProducts;
            order.PriceTotal = 0;
            order.VatTotal = 0;
            order.DiscountTotal = 0;

            foreach (var item in products)
            {
                if (item.OrderId == orderProducts.OrderId)
                {
                    var mainProduct = await _context.Product.FindAsync(item.ProductId);
                    order.PriceTotal = order.PriceTotal + (item.Price * item.Amount);
                    order.VatTotal = order.VatTotal + ((mainProduct.Vat / 100) * (item.Price * item.Amount));
                }
            }
            if (order.DiscountCode != null)
            {
                var discountCodes = from DiscountCodes in _context.DiscountCodes select DiscountCodes;
                foreach (var code in discountCodes)
                {
                    if (code.Code == order.DiscountCode) { order.DiscountTotal = order.PriceTotal * (code.Discount / 100); }
                }
            }
            _context.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool OrderProductsExists(int id)
        {
            return _context.OrderProducts.Any(e => e.Id == id);
        }
    }
}
