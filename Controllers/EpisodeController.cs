#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    public class EpisodeController : Controller
    {
        private readonly CatalogDBContext _context;
private readonly IWebHostEnvironment _hostEnvironment;
        public EpisodeController(CatalogDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Episode
        public async Task<IActionResult> Index()
        {
            return View(await _context.Episode.ToListAsync());
        }

        // GET: Episode/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episode = await _context.Episode
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episode == null)
            {
                return NotFound();
            }

            return View(episode);
        }

        // GET: Episode/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Episode/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestSizeLimit(100_000_000_000)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,S,E,File,FilePath,Timestamp")] Episode episode)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _hostEnvironment.WebRootPath; // wwwroot path
                if (episode.File != null)
                {
                    // Adjust image filename
                    string filename = Path.GetFileNameWithoutExtension(episode.File.FileName); // Filename
                    string extention = Path.GetExtension(episode.File.FileName); // extention
                    string name = filename + DateTime.Now.ToString("yyyyMMddssfff") + extention;
                    string url = Path.Combine("/episodes/" + name);
                    episode.FilePath = url;
                    //Store file
                    using (var FileStream = new FileStream(wwwRoot + "/episodes/" + name, FileMode.Create))
                    {
                        await episode.File.CopyToAsync(FileStream);
                    }
                    //editImages(name);
                }


                _context.Add(episode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(episode);
        }

        // GET: Episode/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episode = await _context.Episode.FindAsync(id);
            if (episode == null)
            {
                return NotFound();
            }
            return View(episode);
        }

        // POST: Episode/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,S,E,FilePath,Timestamp")] Episode episode)
        {
            if (id != episode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(episode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpisodeExists(episode.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(episode);
        }

        // GET: Episode/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episode = await _context.Episode
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episode == null)
            {
                return NotFound();
            }

            return View(episode);
        }

        // POST: Episode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var episode = await _context.Episode.FindAsync(id);
            _context.Episode.Remove(episode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpisodeExists(int id)
        {
            return _context.Episode.Any(e => e.Id == id);
        }
    }
}
