using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.DataBase;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Controllers
{
    public class IncomeController : Controller
    {
        private readonly AppDbContext _context;

        public IncomeController(AppDbContext context)
        {
            _context = context;
        }
        // READ  
        public IActionResult Index()
        {
            var data = _context.Incomes
                .Include(i => i.IncomeCategory)
                .ToList();

            return View(data);
        }

        // CREATE - GET
        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }
        //create
        [HttpPost]
        public IActionResult Create(Income income)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
                return RedirectToAction("Signin", "Home");

            income.UserID = userId.Value;
            if (income.IncomeDate <= new DateTime(1753, 1, 1))
            {
                income.IncomeDate = DateTime.Now;
            }

            _context.Incomes.Add(income);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        // EDIT - GET
        public IActionResult Edit(int id)
        {
            var income = _context.Incomes.Find(id);
            if (income == null)
                return NotFound();

            ViewBag.CategoryList = new SelectList(
                _context.IncomeCategories,
                "IncomeCategoryID",
                "CategoryName",
                income.IncomeCategoryID
            );

            return View(income);
        }
        //edit- post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Income income)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
                return RedirectToAction("Signin", "Home");

            income.UserID = userId.Value;

            // 🔐 Date safety
            if (income.IncomeDate <= new DateTime(1753, 1, 1))
            {
                income.IncomeDate = DateTime.Now;
            }

            _context.Incomes.Update(income);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        //delete
        public IActionResult Delete(int id)
        {
            var income = _context.Incomes.Find(id);
            if (income == null)
                return NotFound();

            _context.Incomes.Remove(income);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        // COMMON METHOD
        private void LoadCategories(int? selectedId = null)
        {
            ViewBag.CategoryList = new SelectList(
                _context.IncomeCategories,
                "IncomeCategoryID",
                "CategoryName",
                selectedId
            );
        }
    }
}
