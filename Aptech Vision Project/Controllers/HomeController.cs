using Aptech_Vision_Project.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

namespace Aptech_Vision_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AptechVisionProjectContext db;

        public HomeController(AptechVisionProjectContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> Index()
        {

            var viewModel = new WebsiteViewModel
            {
                Courses = db.Courses.Include(c => c.CourseinstructorNavigation).Include(c => c.Courselevel),
                Subscriber = new Subscriber()
            };
            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Courses()
        {
            var viewModel = new WebsiteViewModel
            {
                Courses = await db.Courses
                    .Include(c => c.CourseinstructorNavigation)
                    .Include(c => c.Courselevel)
                    .ToListAsync()
            };
            return View(viewModel);
        }





        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();
            TempData["msg"] = "Message Delivered Successfully!";
            return RedirectToAction("Contact");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        public IActionResult Instructor()
        {
            return View();
        }

        public async Task<IActionResult> Coursedetails(int? id)
        {
            if (id == null || db.Courses == null)
            {
                return NotFound();
            }

            var course = await db.Courses.Include(c => c.CourseinstructorNavigation)
                                         .Include(c => c.Courselevel)
                                         .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            var viewModel = new WebsiteViewModel
            {
                Courses = new List<Course> { course } // Wrap the single course in a list
            };

            return View(viewModel);
        }




    }
}
