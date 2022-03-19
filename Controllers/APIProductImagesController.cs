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
    public class APIProductImagesController : ControllerBase
    {
        private readonly CatalogDBContext _context;

        public APIProductImagesController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: api/APIProductImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductImages>>> GetProductImages()
        {
            return await _context.ProductImages.ToListAsync();
        }

        // GET: api/APIProductImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImages>> GetProductImages(int id)
        {
            var productImages = await _context.ProductImages.FindAsync(id);

            if (productImages == null)
            {
                return NotFound();
            }

            return productImages;
        }

        

        private bool ProductImagesExists(int id)
        {
            return _context.ProductImages.Any(e => e.Id == id);
        }
    }
}
