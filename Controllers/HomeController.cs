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
            var todolist = _context.Todos.ToList(); // EntityFramework Listeleme yöntemi
            return View(todolist);
        }

       
    }
}
