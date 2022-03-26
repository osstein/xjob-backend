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
    public class APIEpisode : ControllerBase
    {
        private readonly CatalogDBContext _context;

        public APIEpisode(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: api/APIEpisode
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Episode>>> GetEpisode()
        {
            return await _context.Episode.ToListAsync();
        }

        // GET: api/APIEpisode/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Episode>> GetEpisode(int id)
        {
            var episode = await _context.Episode.FindAsync(id);

            if (episode == null)
            {
                return NotFound();
            }

            return episode;
        }

    

        private bool EpisodeExists(int id)
        {
            return _context.Episode.Any(e => e.Id == id);
        }
    }
}
