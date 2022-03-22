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


        // POST: api/APIOrder (OrderPostRequest? ?)
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderPostRequest>> PostOrder(OrderPostRequest OrderPostRequest)
        {

            //Behöver hämta in discount från discount codes och kontrollera datum, tid och kod
            var discountData = from DiscountCodes in _context.DiscountCodes select DiscountCodes;
            decimal discountFactor = 0;
            decimal totalDiscountedFactor = 1;
            DateTime dateNow = DateTime.Now;
            foreach (var item in discountData)
            {
                if (item.Code == OrderPostRequest.Order.DiscountCode && item.CampaignStart < dateNow && item.CampaignEnd > dateNow)
                {
                    discountFactor = item.Discount / 100;
                    totalDiscountedFactor = 1 - discountFactor;
                }
            }
            //Save instance of order
            OrderPostRequest.Order.OrderNumber = DateTime.Now.ToString("yyyyMMddssfff");
            _context.Order.Add(OrderPostRequest.Order);
            await _context.SaveChangesAsync();

            // Behöver skriva produkter till tabell med Id för order 
            foreach (var item in OrderPostRequest.OrderProducts)
            {
                item.OrderId = OrderPostRequest.Order.Id;;
                _context.OrderProducts.Add(item);
                await _context.SaveChangesAsync();
            }

            //Behöver hämta in moms från produkter och antal från order
            


            //Behöver hämta pris från katalogen


            //Uppdatera instans av order med nya priser hämtade från .NET


            return CreatedAtAction("GetOrder", new { id = OrderPostRequest.Order.Id }, _context.Order);
        }

        //Get calls
        //var context = from CustomerData in _context.CustomerData select CustomerData;
        //var tastingsData = await _context.TastingData.FindAsync(customerData.TastingId);


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