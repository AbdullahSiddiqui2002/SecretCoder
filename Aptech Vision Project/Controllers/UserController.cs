using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using Aptech_Vision_Project.Models;
using Microsoft.EntityFrameworkCore;



namespace Aptech_Vision_Project.Controllers
{
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {
        private readonly AptechVisionProjectContext db;

        public UserController(AptechVisionProjectContext _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            // Extract the UserID from the claims
            var userIdClaim = User.FindFirst("UserID")?.Value;

            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            // Parse the UserID to an integer (or appropriate type)
            int userId = int.Parse(userIdClaim);

            // Fetch the user details from the database using the ID
            var user = db.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                ViewBag.msg = "User not found.";
                return View();
            }

            // Pass the user details to the view
            return View(user);
        }


        public IActionResult Enroll()
        {
            var lectures = db.Lectures.Include(l => l.Course).ToList();
            return View(lectures);
        }

        public IActionResult Enrolled()
        {
            ViewBag.msg = "Successfully Enrolled";
            return View();
        }

    }


}
