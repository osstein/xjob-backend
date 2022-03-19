#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIOrderProductsController : ControllerBase
    {
        private readonly CatalogDBContext _context;

        public APIOrderProductsController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: api/APIOrderProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProducts>>> GetOrderProducts()
        {
            return await _context.OrderProducts.ToListAsync();
        }

        // GET: api/APIOrderProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProducts>> GetOrderProducts(int id)
        {
            var orderProducts = await _context.OrderProducts.FindAsync(id);

            if (orderProducts == null)
            {
                return NotFound();
            }

            return orderProducts;
        }



        // POST: api/APIOrderProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderProducts>> PostOrderProducts(OrderProducts orderProducts)
        {
            _context.OrderProducts.Add(orderProducts);

            // Bör gå att integrera i OrderController



            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderProducts", new { id = orderProducts.Id }, orderProducts);
        }


        private bool OrderProductsExists(int id)
        {
            return _context.OrderProducts.Any(e => e.Id == id);
        }
    }
}


