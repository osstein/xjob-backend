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
    public class APIDiscountCodesController : ControllerBase
    {
        private readonly CatalogDBContext _context;

        public APIDiscountCodesController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: api/APIDiscountCodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscountCodes>>> GetDiscountCodes()
        {
            return await _context.DiscountCodes.ToListAsync();
        }

        // GET: api/APIDiscountCodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountCodes>> GetDiscountCodes(int id)
        {
            var discountCodes = await _context.DiscountCodes.FindAsync(id);

            if (discountCodes == null)
            {
                return NotFound();
            }

            return discountCodes;
        }

        private bool DiscountCodesExists(int id)
        {
            return _context.DiscountCodes.Any(e => e.Id == id);
        }
    }
}
