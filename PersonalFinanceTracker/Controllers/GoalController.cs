using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.DataBase;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Controllers
{
    public class GoalController : Controller
    {
        private readonly AppDbContext _context;
        public GoalController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            int? uid = HttpContext.Session.GetInt32("UserID");
             var goals = _context.Goal.Where(g => g.UserID == uid).ToList();
            return View(goals);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Goal goal)
        {
            int? uid = HttpContext.Session.GetInt32("UserID");
            goal.UserID = uid.Value;
            _context.Goal.Add(goal);
            _context.SaveChanges();
            ViewBag.mess = "Goal is SET !!";
            return RedirectToAction("Index");
        }
       // [HttpGet]
        public IActionResult Delete(int id)
        {
            var g = _context.Goal.Find(id);
            if (g == null)
                return NotFound();
            _context.Goal.Remove(g);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
