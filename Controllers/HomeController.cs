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
            var todolist = _context.Todos.ToList(); // EntityFramework Listeleme yöntemi
            ViewBag.Id = Id;
            return View(todolist);
        }

        public IActionResult Details(int Id)
        {
            var details = _context.Todos.Find(Id); // EntityFramework Seçili Id'yi bulma. (SELECT * FROM Todos Where Id=@Id) gibi

            if (details != null)
            {
                return View(details);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddTodo(Todo todo)
        {
            _context.Todos.Add(todo); // EntityFrameWork Ekleme Ýþlemi
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTodo(Todo todo)
        {
            _context.Todos.Remove(todo); // EntityFramework Silme Ýþlemi
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditTodo(Todo todo)
        {

            _context.Todos.Update(todo); // EntityFrameWork Update Ýþlemi
            _context.SaveChanges();
            return RedirectToAction("Index");



        }

        public IActionResult MarkComplete(int id)
        {
            //_connection.Execute("UPDATE TBLTodo SET IsApproved = 1 WHERE Id = @id", new { id });
            //return RedirectToAction("Index");
            return RedirectToAction("Index");
        }

        public IActionResult MarkInComplete(int id)
        {
            //_connection.Execute("UPDATE TBLTodo SET IsApproved = 0 WHERE Id = @id", new { id });
            return RedirectToAction("Index");
        }





    }
}
