using Aptech_Vision_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Aptech_Vision_Project.Controllers
{
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    [Authorize(Roles = "admin")]
    public class CourseController : Controller
    {
        private readonly AptechVisionProjectContext db;

        public CourseController(AptechVisionProjectContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> Index()
        {
            var aptechVisionProjectContext = db.Courses.Include(c => c.CourseinstructorNavigation).Include(c => c.Courselevel);
            return View(await aptechVisionProjectContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Courses == null)
            {
                return NotFound();
            }

            var course = await db.Courses
                .Include(c => c.CourseinstructorNavigation)
                .Include(c => c.Courselevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        public IActionResult Create()
        {
            ViewBag.Instructor = new SelectList(db.Instructors, "Id", "Instructorname");
            ViewBag.Courselevel = new SelectList(db.Courselevels, "Id", "Courselevel1");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course, IFormFile file)
        {
            string imagename = DateTime.Now.ToString("yymmddhhmmss");//2410152541245412
            imagename += "-" + Path.GetFileName(file.FileName);//2410152541245412-sonata.jpg

            var imagepath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Admin/img/courses");
            var imageValue = Path.Combine(imagepath, imagename);

            using (var stream = new FileStream(imageValue, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var dbimage = Path.Combine("/Admin/img/courses", imagename);//Uploads/2410152541245412-sonata.jpg

            course.Courseimage = dbimage;

            db.Courses.Add(course);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Courses == null)
            {
                return NotFound();
            }

            var course= await db.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.Instructor = new SelectList(db.Instructors, "Id", "Instructorname");
            ViewBag.Courselevel = new SelectList(db.Courselevels, "Id", "Courselevel1");
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course, IFormFile file, string oldImage)
        {
            if (file != null && file.Length > 0)
            {
                string imagename = DateTime.Now.ToString("yymmddhhmmss");//2410152541245412
                imagename += "-" + Path.GetFileName(file.FileName);//2410152541245412-sonata.jpg

                var imagepath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Admin/img/courses");
                var imageValue = Path.Combine(imagepath, imagename);

                using (var stream = new FileStream(imageValue, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var dbimage = Path.Combine("/Admin/img/courses", imagename);//Uploads/2410152541245412-sonata.jpg

                course.Courseimage = dbimage;
                db.Courses.Update(course);
                db.SaveChanges();
            }
            else
            {
                course.Courseimage = oldImage;
                db.Courses.Update(course);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

     

        public IActionResult Delete(int id)
        {
            var item = db.Courses.Find(id);

            if (item != null)
            {
                var course = db.Courses
                    .Include(c => c.CourseinstructorNavigation)
                    .Include(c => c.Courselevel)
                    .FirstOrDefault(m => m.Id == id);

                ViewBag.Item = item;
                ViewBag.Course = course;

                return View(item); // Assuming your existing model directive is for 'item'
            }
            else
            {
                return RedirectToAction("Index"); // Replace "Index" with the actual action name.
            }
        }


        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var course = db.Courses
                .FirstOrDefault(i => i.Id == id);  // No need for Include since you're loading lectures separately

            if (course != null)
            {
                // Load lectures associated with the course
                var lectures = db.Lectures
                    .Where(c => c.Courseid == course.Id)  // Filter lectures by Course ID
                    .ToList();

                if (lectures != null && lectures.Any())
                {
                    // Remove all found lectures
                    db.Lectures.RemoveRange(lectures);
                }

                // Remove the course
                db.Courses.Remove(course);

                // Save all changes at once
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }





    }
}
