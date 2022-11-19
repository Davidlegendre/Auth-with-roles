using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SecurityProject.DB;
using SecurityProject.DB.Models;
using SecurityProject.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace SecurityProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly SecurityContext _context;
        public AuthController(SecurityContext context) {
            _context = context;
        }
        [Authorize(Roles = "Administrador")]
        [ResponseCache(NoStore = true, Duration = 0)]
        public IActionResult Registro()
        {
            var roles = _context.Roles.Where(e => e.RolDescription != "Administrador");
            ViewData["RolID"] = new SelectList(roles, "RolID", "RolDescription");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroModel user)
        {
            if (!ModelState.IsValid)
            {
                ViewData["RolID"] = new SelectList(_context.Roles, "RolID", "RolDescription", user.RolID);
                return View(user);
            }

            var isExist = await _context.Users.FirstOrDefaultAsync(e => e.UserEmail == user.UserEmail || e.RolID == user.RolID);
            if (isExist != null) {
                ViewBag.msn = "Error de Registro, puede que ya exista un usuario con ese email o que ya haya un admin";
                return View(user);
            }

            _context.Add(new User()
            {
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                UserLastName = user.UserLastName,
                UserPassword = user.UserPassword,
                RolID = user.RolID
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("DashboardAdmin", "Home");

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<IActionResult> Login([Bind("UserEmail,UserPassword")] LoginModel login) { 
            if(!ModelState.IsValid)
            {
                return View(login);
            }

            var result = await _context.Users.FirstOrDefaultAsync(e => e.UserEmail == login.UserEmail && e.UserPassword == login.UserPassword);
            var rol = await _context.Roles.FirstOrDefaultAsync(e => e.RolID == result.RolID);
            if(result != null)
            {
                //para crear la cookie es como el token
                var claims = new List<Claim>() { 
                    new Claim(ClaimTypes.Name, result.UserName),
                    new Claim("Email", result.UserEmail),
                    new Claim(ClaimTypes.Role, rol.RolDescription)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

               await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
               
                return RedirectToAction(rol.RolDescription == "Administrador" ? "DashboardAdmin" : "DashboardUser", "Home");
            }            
            ViewBag.msn = "Login Incorrecto";
            return View(login);
        }

        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
