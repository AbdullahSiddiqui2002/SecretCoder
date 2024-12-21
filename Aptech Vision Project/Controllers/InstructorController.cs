using Aptech_Vision_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Aptech_Vision_Project.Controllers
{
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    [Authorize(Roles = "admin")]
    public class InstructorController : Controller
    {

        private readonly AptechVisionProjectContext db;

        public InstructorController(AptechVisionProjectContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var data = db.Instructors.ToList();
            return View(data);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Instructors == null)
            {
                return NotFound();
            }

            var instructor = await db.Instructors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Instructor instructor, IFormFile file)
        {
            string imagename = DateTime.Now.ToString("yymmddhhmmss");
            imagename += "-" + Path.GetFileName(file.FileName);

            var imagepath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Admin/img/instructor");
            var imageValue = Path.Combine(imagepath, imagename);

            using (var stream = new FileStream(imageValue, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var dbimage = Path.Combine("/Admin/img/instructor", imagename);

            instructor.Instructorimage = dbimage;

            db.Instructors.Add(instructor);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Instructors == null)
            {
                return NotFound();
            }

            var instructor = await db.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        [HttpPost]
        public IActionResult Edit(Instructor instructor, IFormFile file, string oldImage)
        {
            if (file != null && file.Length > 0)
            {
                string imagename = DateTime.Now.ToString("yymmddhhmmss");
                imagename += "-" + Path.GetFileName(file.FileName);

                var imagepath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Admin/img/instructor");
                var imageValue = Path.Combine(imagepath, imagename);

                using (var stream = new FileStream(imageValue, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var dbimage = Path.Combine("/Admin/img/instructor", imagename);

                instructor.Instructorimage = dbimage;
                db.Instructors.Update(instructor);
                db.SaveChanges();
            }
            else
            {
                instructor.Instructorimage = oldImage;
                db.Instructors.Update(instructor);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var instructor = db.Instructors.Find(id);
            if (instructor != null)
            {
                return View(instructor);
            }
            else
            {
                return RedirectToAction("Index"); // Ensure a valid action name here
            }
        }



        [HttpPost]
        public IActionResult DeleteConfirmed(int id) // Renamed method for clarity
        {
            var instructor = db.Instructors
                .Include(i => i.Courses) // Include Courses navigation property
                .FirstOrDefault(i => i.Id == id);

            if (instructor != null)
            {
                // Find all courses where the instructor ID matches
                var courses = db.Courses
                    .Where(c => c.Courseinstructor == instructor.Id)
                    .ToList();

                if (courses != null && courses.Any())
                {
                    // For each course, find and remove related lectures
                    foreach (var course in courses)
                    {
                        var lectures = db.Lectures
                            .Where(l => l.Courseid == course.Id)
                            .ToList();

                        if (lectures != null && lectures.Any())
                        {
                            db.Lectures.RemoveRange(lectures);
                        }
                    }

                    // Remove all found courses
                    db.Courses.RemoveRange(courses);
                }

                // Remove the instructor
                db.Instructors.Remove(instructor);

                // Save all changes at once
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }



    }
}
