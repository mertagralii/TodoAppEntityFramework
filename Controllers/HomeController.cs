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

        public IActionResult Details(int Id) 
        {
            var details = _context.Todos.Find(Id); // EntityFramework Seçili Id'yi bulma. (SELECT * FROM Todos Where Id=@Id) gibi

            if(details != null)
            {
                return View(details);
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddTodo() 
        {
            
        }
    }
}
