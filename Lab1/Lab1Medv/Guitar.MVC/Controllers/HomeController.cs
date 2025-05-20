using Microsoft.AspNetCore.Mvc;
using Guitar.Infrastructure;

namespace Guitar.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly GuitarContext _context;

        public HomeController(GuitarContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Показати всі гітари
            var guitars = _context.Guitars.ToList();
            return View(guitars);
        }
    }
}