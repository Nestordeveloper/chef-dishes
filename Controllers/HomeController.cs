using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using chef_dishes.Models;

namespace chef_dishes.Controllers;

public class HomeController : Controller
{
    public static List<Dish> AllDishes = new List<Dish>();
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;
    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        List<Chef> chefs = _context.Chefs.ToList();

        foreach (var chef in chefs)
        {
            chef.AllDishes = _context.Dishes.Where(d => d.Creator == chef).ToList();
        }

        return View(chefs);
    }
    [HttpGet]
    [Route("dishes")]
    public IActionResult Dishes()
    {
        List<Dish> dishes = _context.Dishes.Include(d => d.Creator).ToList();

        return View("Dishes", dishes);
    }
    [HttpGet]
    [Route("dishes/new")]
    public IActionResult Dish()
    {
        List<Chef> chefs = _context.Chefs.ToList();

        ViewBag.ChefList = chefs.Select(c => new SelectListItem
        {
            Text = $"{c.FirstName} {c.LastName}",
            Value = c.ChefId.ToString()
        }).ToList();

        return View("Dish");
    }
    [HttpPost]
    [Route("dishes/createdish")]
    public IActionResult CreateDish(Dish newDish)
    {
        if (ModelState.IsValid)
        {
            _context.Dishes.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Dishes");
        }
        else
        {
            return View("Dish");
        }
    }

    [HttpGet]
    [Route("chefs/new")]
    public IActionResult Chef()
    {
        return View("Chef");
    }

    [HttpPost]
    [Route("chefs/createchef")]
    public IActionResult CreateChef(Chef newChef)
    {
        if (ModelState.IsValid)
        {
            _context.Chefs.Add(newChef);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            return View("Chef");
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
