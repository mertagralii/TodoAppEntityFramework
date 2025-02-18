using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAppEntityFramework.Data;
using TodoAppEntityFramework.Models;

namespace TodoAppEntityFramework.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? Id)
        {
            var todolist = _context.Todos.ToList(); // EntityFramework Listeleme y�ntemi
            ViewBag.Id = Id;
            return View(todolist);
        }

        public IActionResult Details(int Id)
        {
            var details = _context.Todos.Find(Id); // EntityFramework Se�ili Id'yi bulma. (SELECT * FROM Todos Where Id=@Id) gibi

            if (details != null)
            {
                return View(details);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddTodo(Todo todo)
        {
            _context.Todos.Add(todo); // EntityFrameWork Ekleme ��lemi
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTodo(Todo todo)
        {
            _context.Todos.Remove(todo); // EntityFramework Silme ��lemi
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditTodo(Todo todo)
        {

            _context.Todos.Update(todo); // EntityFrameWork Update ��lemi
            _context.SaveChanges();
            return RedirectToAction("Index");



        }

        public IActionResult MarkComplete(int id)
        {
            var todo = _context.Todos.Find(id); // Se�ili Id'nin verilerini getir
            if (todo != null)  // gelen Id verileri todo de�i�kenine gelmi� ve bo� de�ilse
            {
                todo.IsApproved = true; // O Id'ye ait verilerin i�indeki IsApproved de�erini true yap
                _context.SaveChanges(); // veritaban�nda yap�lan de�i�ikli�i kaydet.
            }
            return RedirectToAction("Index"); // Anasayfaya geri d�n.
        }

        public IActionResult MarkInComplete(int id) // Yine ayn� i�lem.
        {
           var todo = _context.Todos.Find(id);
            if (todo != null) 
            {
                todo.IsApproved = false;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }





    }
}
