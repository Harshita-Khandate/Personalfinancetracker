using System.Diagnostics;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.DataBase;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Controllers
{
    public class HomeController : Controller
    {
       // private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;//new

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(AppDbContext context)//new
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(Users u)
        {
           if(!ModelState.IsValid)
            {
                return View(u);
            }
           _context.Add(u);
            _context .SaveChanges();

            return RedirectToAction("Signin");
        }

        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signin(string Email,string Password)
        {
            var check=_context.Users.FirstOrDefault(u=>u.Email == Email && u.Password==Password);

            if(check==null)
            {
                ViewBag.Error = "Invalid Email and Password";
                return View();
            }
           // HttpContext.Session.SetString("Name", check.Name);
           // HttpContext.Session.SetInt32("UserID", check.UserID);
            return RedirectToAction("Dashboard");
        }

        public IActionResult Dashboard()
        {
            //if (HttpContext.Session.GetString("Name") == null)
            //{
            //    return RedirectToAction("Signin");
            //}

            //ViewBag.Name = HttpContext.Session.GetString("Name");
            return View();
        }

        public IActionResult Logout()
        {
            //HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
