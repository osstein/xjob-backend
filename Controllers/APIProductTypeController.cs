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
    public class APIProductTypeController : ControllerBase
    {
        private readonly CatalogDBContext _context;

        public APIProductTypeController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: api/APIProductType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductType()
        {
            return await _context.ProductType.ToListAsync();
        }

        // GET: api/APIProductType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductType>> GetProductType(int id)
        {
            var productType = await _context.ProductType.FindAsync(id);

            if (productType == null)
            {
                return NotFound();
            }

            return productType;
        }

        

        private bool ProductTypeExists(int id)
        {
            return _context.ProductType.Any(e => e.Id == id);
        }
    }
}
