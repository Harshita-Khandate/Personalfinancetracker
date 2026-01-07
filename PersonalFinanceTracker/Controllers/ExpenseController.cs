using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalFinanceTracker.DataBase;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly AppDbContext _context;

        public ExpenseController(AppDbContext context)
        {
            _context = context;
        }

        // 🔁 COMMON METHOD
        private void LoadCategories()
        {
            ViewBag.catlist = new SelectList(
                _context.ExpenseCategory.ToList(),
                "ExpenseCategoryID",
                "CategoryName"
            );
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Signin", "Home");
            }

            LoadCategories();
            return View(new Expense()); // ✅ IMPORTANT
        }

        [HttpPost]
        public IActionResult Index(Expense e)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Signin", "Home");
            }

            if (!ModelState.IsValid)
            {
                LoadCategories();   // ✅ MUST
                return View(e);
            }

            e.UserID = userId.Value;
           // e.ExpenseDate = DateTime.Now;

            _context.Expense.Add(e);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
