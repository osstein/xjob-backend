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
    public class APICatalogCategoriesController : ControllerBase
    {
        private readonly CatalogDBContext _context;

        public APICatalogCategoriesController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: api/APICatalogCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogCategories>>> GetCatalogCategories()
        {
            return await _context.CatalogCategories.ToListAsync();
        }

        // GET: api/APICatalogCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogCategories>> GetCatalogCategories(int id)
        {
            var catalogCategories = await _context.CatalogCategories.FindAsync(id);

            if (catalogCategories == null)
            {
                return NotFound();
            }

            return catalogCategories;
        }


        private bool CatalogCategoriesExists(int id)
        {
            return _context.CatalogCategories.Any(e => e.Id == id);
        }
    }
}
