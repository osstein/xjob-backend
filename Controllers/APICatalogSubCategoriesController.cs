/* #nullable disable
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
    public class APICatalogSubCategoriesController : ControllerBase
    {
        private readonly CatalogDBContext _context;

        public APICatalogSubCategoriesController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: api/APICatalogSubCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogSubCategories>>> GetCatalogSubCategories()
        {
            return await _context.CatalogSubCategories.ToListAsync();
        }

        // GET: api/APICatalogSubCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogSubCategories>> GetCatalogSubCategories(int id)
        {
            var catalogSubCategories = await _context.CatalogSubCategories.FindAsync(id);

            if (catalogSubCategories == null)
            {
                return NotFound();
            }

            return catalogSubCategories;
        }

        private bool CatalogSubCategoriesExists(int id)
        {
            return _context.CatalogSubCategories.Any(e => e.Id == id);
        }
    }
}
 */