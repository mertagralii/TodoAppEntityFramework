using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TodoAppEntityFramework.Data;
using TodoAppEntityFramework.Models;

namespace TodoAppEntityFramework.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext  _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
