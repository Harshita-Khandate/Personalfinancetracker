using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;//for seletedList...Dropdown
using Microsoft.EntityFrameworkCore;//Used for Include(), LINQ, and EF Core features
using PersonalFinanceTracker.DataBase;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly AppDbContext _context;//_context is the database connection object
        public ExpenseController(AppDbContext context)
        {
            _context = context;
        }
//read
        public IActionResult Index()
        {
            var data = _context.Expenses
                .Include(i => i.ExpenseCategory)
                .ToList();
            int? userid = HttpContext.Session.GetInt32("UserID");
            decimal totalExpense = _context.Expenses
    .Where(i => i.UserID == userid)
    .Select(i => (decimal?)i.Amount)
    .Sum() ?? 0;
            ViewBag.TotalExpense = totalExpense;
            return View(data);
        }
        //crete - get
        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }
//crete -post
        [HttpPost]
        public IActionResult Create(Expense expense)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
                return RedirectToAction("Signin", "Home");

            expense.UserID = userId.Value;

            if (expense.ExpenseDate <= new DateTime(1753, 1, 1))
            {
                expense.ExpenseDate = DateTime.Now;
            }
            _context.Expenses.Add(expense);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //edit-get
        public IActionResult Edit(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null)
                return NotFound();
            ViewBag.CategoryList = new SelectList(
                _context.ExpenseCategories,
                "ExpenseCategoryID",
                "CategoryName",
                expense.ExpenseCategoryID
            );
            return View(expense);
        }
        //edit-post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Expense expense)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
                return RedirectToAction("Signin", "Home");
            expense.UserID = userId.Value;
            if (expense.ExpenseDate <= new DateTime(1753, 1, 1))
            {
                expense.ExpenseDate = DateTime.Now;
            }
            _context.Expenses.Update(expense);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
         //delete
        public IActionResult Delete(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null)
                return NotFound();
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //comman method
        private void LoadCategories(int? selectedId = null)
        {
            ViewBag.CategoryList = new SelectList(
                _context.ExpenseCategories,
                "ExpenseCategoryID",
                "CategoryName",
                selectedId
            );
        }
}
}
