using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalFinanceTracker.DataBase;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Controllers
{
    public class IncomeController : Controller
    {
        private readonly AppDbContext _context;//new
        public IncomeController(AppDbContext context)//new
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Signin", "Home");
            }
            // Load categories for dropdown
            ViewBag.catlist = new SelectList(
                _context.IncomeCategory.ToList(),
                "IncomeCategoryID",
                "CategoryName"
            );
            return View();
        }
        [HttpPost]
        public IActionResult Index(Income income)
        {
            // Session check again (VERY IMPORTANT)
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Signin", "Home");
            }
            if (!ModelState.IsValid)
            {
                // Reload dropdown if validation fails
                ViewBag.catlist = new SelectList(
                    _context.IncomeCategory.ToList(),
                    "IncomeCategoryID",
                    "CategoryName"
                );
                return View(income);
            }

            // ✅ Attach logged-in user
            income.UserID = userId.Value;
           // income.IncomeDate = DateTime.Now;

            _context.Income.Add(income);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
