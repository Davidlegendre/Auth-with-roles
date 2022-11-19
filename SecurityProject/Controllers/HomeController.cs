using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityProject.Models;

using System.Diagnostics;
using System.Net;
using System.Security.Claims;

namespace SecurityProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        public IActionResult Privacy()
        {
            return View();
        }


        [Authorize(Roles = "Administrador")]
        [ResponseCache(NoStore = true, Duration = 0)]
        public IActionResult DashboardAdmin()
        {
            return RedirectToAction("Index", "Users");
        }


        [Authorize(Roles = "Usuario")]
        [ResponseCache(NoStore = true, Duration = 0)]
        public IActionResult DashboardUser()
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