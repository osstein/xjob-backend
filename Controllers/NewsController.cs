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
    public class NewsController : Controller
    {
        private readonly CatalogDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public NewsController(CatalogDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: News
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var NewsList = from News in _context.News
                           select News;
            if (!String.IsNullOrEmpty(searchString))
            {
                NewsList = NewsList.Where(s => s.Title.ToLower().Contains(searchString.ToLower()) ||  s.Content.ToLower().Contains(searchString.ToLower()) );
            }


            ViewData["count"] = NewsList.Count();
            switch (sortOrder)
            {
                case "name_desc":
                    NewsList = NewsList.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    NewsList = NewsList.OrderBy(s => s.Timestamp);
                    break;
                case "date_desc":
                    NewsList = NewsList.OrderByDescending(s => s.Timestamp);
                    break;
                default:
                    NewsList = NewsList.OrderBy(s => s.Title);
                    break;
            }
            return View(await NewsList.ToListAsync());
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,ImagePath,ImageFile,ImageAlt,Timestamp")] News news)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _hostEnvironment.WebRootPath; // wwwroot path
                if (news.ImageFile != null)
                {
                    // Adjust image filename
                    string filename = Path.GetFileNameWithoutExtension(news.ImageFile.FileName); // Filename
                    string extention = Path.GetExtension(news.ImageFile.FileName); // extention
                    string name = filename + DateTime.Now.ToString("yyyyMMddssfff") + extention;
                    string url = Path.Combine("/images/" + name);
                    news.ImagePath = url;
                    //Store file
                    using (var FileStream = new FileStream(wwwRoot + "/images/" + name, FileMode.Create))
                    {
                        await news.ImageFile.CopyToAsync(FileStream);
                    }
                    //editImages(name);
                }

                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,ImagePath,ImageFile,ImageAlt,Timestamp")] News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string wwwRoot = _hostEnvironment.WebRootPath; // wwwroot path
                if (news.ImageFile != null)
                {
                    // Adjust image filename
                    string filename = Path.GetFileNameWithoutExtension(news.ImageFile.FileName); // Filename
                    string extention = Path.GetExtension(news.ImageFile.FileName); // extention
                    string name = filename + DateTime.Now.ToString("yyyyMMddssfff") + extention;
                    string url = Path.Combine("/images/" + name);
                    news.ImagePath = url;
                    //Store file
                    using (var FileStream = new FileStream(wwwRoot + "/images/" + name, FileMode.Create))
                    {
                        await news.ImageFile.CopyToAsync(FileStream);
                    }
                    //editImages(name);
                }
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
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
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
