using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MediaDashboardPro.Models;

namespace MediaDashboardPro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }
        
        public IActionResult UneteAlEquipo()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            return View();
        }

        public IActionResult UserProfile()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ConfigurationPanel()
        {
            return View();
        }

        public IActionResult MediaDashboard()
        {
            return View();
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
