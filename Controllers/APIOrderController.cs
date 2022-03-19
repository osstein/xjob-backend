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
    public class APIOrderController : ControllerBase
    {
        private readonly CatalogDBContext _context;

        public APIOrderController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: api/APIOrder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return await _context.Order.ToListAsync();
        }

        // GET: api/APIOrder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }


        // POST: api/APIOrder
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderPostRequest OrderPostRequest)
        {
            
            //Behöver hämta in discount från discount codes
            var discountData = from DiscountCodes in _context.DiscountCodes select DiscountCodes;
            decimal discountFactor = 0;
            decimal totalDiscountedFactor = 1; 
            foreach (var item in discountData)
            {
                if (item.Code == OrderPostRequest.Order.DiscountCode)
                {
                    discountFactor = item.Discount / 100;
                    totalDiscountedFactor = 1 - discountFactor;
                }
            }
            // Behöver skriva produkterr till tabell med Id för order 
            var OrderProducts = await _context.OrderProducts.ToListAsync(order);
            //Behöver hämta in moms från produkter och antal från order

            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            //Behöver hämta pris från katalogen
            return createdObject;
        }



        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
/* // PUT: api/APIProductType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductType(int id, ProductType productType)
        {
            if (id != productType.Id)
            {
                return BadRequest();
            }

            _context.Entry(productType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/