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
    public class APIProductPropertiesController : ControllerBase
    {
        private readonly CatalogDBContext _context;

        public APIProductPropertiesController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: api/APIProductProperties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductProperties>>> GetProductProperties()
        {
            return await _context.ProductProperties.ToListAsync();
        }

        // GET: api/APIProductProperties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductProperties>> GetProductProperties(int id)
        {
            var productProperties = await _context.ProductProperties.FindAsync(id);

            if (productProperties == null)
            {
                return NotFound();
            }

            return productProperties;
        }

        

        private bool ProductPropertiesExists(int id)
        {
            return _context.ProductProperties.Any(e => e.Id == id);
        }
    }
}
