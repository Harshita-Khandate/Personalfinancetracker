using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.DataBase;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Home page 
        public IActionResult Index()
        {
            return View();
        }

        //SIGN UP

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(Users u)
        {
            if (!ModelState.IsValid)
            {
                return View(u);
            }

            _context.Users.Add(u);
            _context.SaveChanges();

            return RedirectToAction("Signin");
        }

        //SIGN IN

        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signin(string Email, string Password)
        {
            var check = _context.Users
                               .FirstOrDefault(u => u.Email == Email && u.Password == Password);

            if (check == null)
            {
                ViewBag.Error = "Invalid Email or Password";
                return View();
            }

            //Store data in session
            HttpContext.Session.SetInt32("UserID", check.UserID);
            HttpContext.Session.SetString("Name", check.Name);

            return RedirectToAction("Dashboard");
        }

        //DASHBOARD
        public IActionResult Dashboard()
        {
            //Session check
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Signin");
            }
            ViewBag.Name = HttpContext.Session.GetString("Name");
            return View();
        }
        //LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // clear all session data
            return RedirectToAction("Signin");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
