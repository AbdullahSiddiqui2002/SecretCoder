using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using Aptech_Vision_Project.Models;

namespace Aptech_Vision_Project.Controllers
{
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly AptechVisionProjectContext db;

        public AdminController(AptechVisionProjectContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            // Prevent browser caching
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            var courseCount = db.Courses.Count();
            var instructorCount = db.Instructors.Count();

            // Create a view model to pass the data to the view
            var viewModel = new AdminDashboardViewModel
            {
                TotalCourses = courseCount,
                TotalInstructors = instructorCount
            };

            return View(viewModel);
        }

       
    }
}
