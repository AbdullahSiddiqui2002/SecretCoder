using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Aptech_Vision_Project.Models;
using Microsoft.AspNetCore.Identity;

namespace Aptech_Vision_Project.Controllers
{
    public class AuthController : Controller
    {
        private readonly AptechVisionProjectContext db;

        public AuthController(AptechVisionProjectContext _db)
        {
            db = _db;
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(User user)
        {

                var checkUser = db.Users.FirstOrDefault(a => a.Email == user.Email);
                if (checkUser == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login");

                }
                else
                {
                    ViewBag.msg = "User Already registered. Please Login";
                    return View();
                }
         
        }

        public IActionResult Login()
        {
            // Prevent caching of the login page
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            // Redirect if the user is already authenticated
            if (User.Identity?.IsAuthenticated == true)
            {
                // Redirect based on role
                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (User.IsInRole("user"))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View();
            }

            return View();
        }

        [HttpPost]
public IActionResult Login(string email, string password)
{
    // Check if email or password are null or empty
    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
        ViewBag.msg = "Email and password cannot be empty.";
        return View();
    }

    // Fetch the user from the database
    var user = db.Users.SingleOrDefault(u => u.Email == email && u.Pass == password);

    if (user != null)
    {
        // Check the role of the user
        var claims = new List<Claim>
        {
            new Claim("UserID", user.Id.ToString()),
            new Claim("Email", user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("Name", user.Firstname + " " + user.Lastname)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        if (user.Role == "admin")
        {
            return RedirectToAction("Index", "Admin");
        }
        else if (user.Role == "user")
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ViewBag.msg = "Role not recognized.";
            return View();
        }
    }
    else
    {
        ViewBag.msg = "Invalid credentials. Please check your email and password.";
        return View();
    }
}


        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete(".AspNetCore.Cookies");
            return RedirectToAction("Login", "Auth");
        }
    }
}
