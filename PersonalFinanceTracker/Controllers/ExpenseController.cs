using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Signin", "Home");
            }

           // LoadCategories();
            //Expense e=new Expense();
            //if(id!=null)
            //{
            //    e = _context.Expense.Find(id);
            //}
            //ViewBag.expenseList = _context.Expense
            //    .Include(x => x.ExpenseCategory)
            //    .Where(x => x.UserID == userId)
            //    .ToList();
            return View(); //e
        }

        [HttpPost]
        public IActionResult Index(Expense e)
        {
            foreach (var item in ModelState)
            {
                foreach (var error in item.Value.Errors)
                {
                    Console.WriteLine($"{item.Key} : {error.ErrorMessage}");
                }
            }

            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Signin", "Home");
            }
            e.UserID = userId.Value;
            if (!ModelState.IsValid)
            {
               // LoadCategories();
               

                //ViewBag.expenseList = _context.Expense
                //   .Include(x => x.ExpenseCategory)
                //   .Where(x => x.UserID == userId)
                //   .ToList();
                 return View(e);
            }
            //  if (e.ExpenseID == 0)
            // {
            //   _context.Expense.Add(e);
            // }
            // else
            // {
            // _context.Expense.Update(e);

            // }
            e.UserID = 1;
            e.ExpenseCategoryID = 2;
            e.ExpenseDate = DateTime.Today;

           // if (e.ExpenseID == 0)
           // {
                _context.Expense.Add(e);   // INSERT
           // }
            //else
            //{
            //    _context.Expense.Update(e); // UPDATE
            //}
            Console.WriteLine("UserID: " + e.UserID);
            Console.WriteLine("ExpenseDate: " + e.ExpenseDate);
            Console.WriteLine("Amount: " + e.Amount);
            Console.WriteLine("CategoryID: " + e.ExpenseCategoryID);

            try
            {
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return Content(ex.InnerException?.Message ?? ex.Message);
            }


            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var e = _context.Expense.Find(id);
            if (e != null)
            {
                _context.Expense.Remove(e);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
