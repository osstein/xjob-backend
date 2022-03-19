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
    public class APIProductColorController : ControllerBase
    {
        private readonly CatalogDBContext _context;

        public APIProductColorController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: api/APIProductColor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductColor>>> GetProductColor()
        {
            return await _context.ProductColor.ToListAsync();
        }

        // GET: api/APIProductColor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductColor>> GetProductColor(int id)
        {
            var productColor = await _context.ProductColor.FindAsync(id);

            if (productColor == null)
            {
                return NotFound();
            }

            return productColor;
        }

       

        private bool ProductColorExists(int id)
        {
            return _context.ProductColor.Any(e => e.Id == id);
        }
    }
}
