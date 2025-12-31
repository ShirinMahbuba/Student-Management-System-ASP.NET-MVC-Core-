using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using webapp04.Models;

namespace webapp04.Controllers
{
    public class HomeController : Controller
    {
        

        private readonly ILogger<HomeController> _logger;
 private readonly webapp04Context _context;
 public HomeController(ILogger<HomeController> logger,webapp04Context context)
 {
     _logger = logger;
     _context = context;

 }

 public IActionResult Index()
 {
     var result = _context.Student.ToList();
     return View(result);
 }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
