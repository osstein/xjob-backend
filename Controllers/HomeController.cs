using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Data;

namespace backend.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CatalogDBContext _context;

    public HomeController(ILogger<HomeController> logger, CatalogDBContext context)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Report()
    {
        var order = from Order in _context.Order select Order;

        order.ToList();

        decimal? totalAllTime = 0;
        decimal? totalVatAllTime = 0;
        decimal? totalDiscountAllTime = 0;
        foreach (var item in order)
        {
            totalAllTime += item.PriceTotal;
            totalVatAllTime += item.VatTotal;
            totalDiscountAllTime += item.DiscountTotal;

        }

        ViewData["totalAllTime"] = totalAllTime;
        ViewData["totalVatAllTime"] = totalVatAllTime;
        ViewData["totalDiscountAllTime"] = totalDiscountAllTime;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
