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
    public class APIProductSizeController : ControllerBase
    {
        private readonly CatalogDBContext _context;

        public APIProductSizeController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: api/APIProductSize
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSize>>> GetProductSize()
        {
            return await _context.ProductSize.ToListAsync();
        }

        // GET: api/APIProductSize/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSize>> GetProductSize(int id)
        {
            var productSize = await _context.ProductSize.FindAsync(id);

            if (productSize == null)
            {
                return NotFound();
            }

            return productSize;
        }

    
        private bool ProductSizeExists(int id)
        {
            return _context.ProductSize.Any(e => e.Id == id);
        }
    }
}
