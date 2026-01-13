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

        // ================================
        // SHOW + EDIT
        // ================================
        public IActionResult Index(int? id)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
                return RedirectToAction("Signin", "Home");

            // Dropdown
            ViewBag.catlist = new SelectList(
                _context.IncomeCategory.ToList(),
                "IncomeCategoryID",
                "CategoryName"
            );

            // Edit mode
            Income income = new Income();
            if (id != null)
            {
                income = _context.Income.Find(id);
            }

            // List
            ViewBag.incomeList = _context.Income
                .Include(x => x.IncomeCategory)
                .Where(x => x.UserID == userId)
                .ToList();

            return View(income);
        }

        // ================================
        // INSERT + UPDATE
        // ================================
        [HttpPost]
        public IActionResult Index(Income income)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
                return RedirectToAction("Signin", "Home");

            // Assign session value BEFORE validation
            income.UserID = userId.Value;

            if (!ModelState.IsValid)
            {
                ViewBag.catlist = new SelectList(
                    _context.IncomeCategory.ToList(),
                    "IncomeCategoryID",
                    "CategoryName"
                );

                ViewBag.incomeList = _context.Income
                    .Include(x => x.IncomeCategory)
                    .Where(x => x.UserID == userId)
                    .ToList();

                return View(income);
            }

            if (income.IncomeID == 0)
                _context.Income.Add(income);
            else
                _context.Income.Update(income);
            Console.WriteLine(income.Amount);
            Console.WriteLine(income.UserID);
            Console.WriteLine(income.IncomeCategoryID);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ================================
        // DELETE
        // ================================
        public IActionResult Delete(int id)
        {
            var income = _context.Income.Find(id);
            if (income != null)
            {
                _context.Income.Remove(income);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
