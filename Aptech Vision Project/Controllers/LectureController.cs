using Aptech_Vision_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Aptech_Vision_Project.Controllers
{
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    [Authorize(Roles = "admin")]
    public class LectureController : Controller
    {
        private readonly AptechVisionProjectContext db;

        public LectureController(AptechVisionProjectContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            // Eagerly load the Course entity related to each Lecture
            var lectures = db.Lectures.Include(l => l.Course).ToList();
            return View(lectures);
        }

        public IActionResult Create()
        {
            ViewBag.Courses = new SelectList(db.Courses, "Id", "Coursename");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Lecture lecture)
        {
           
            db.Lectures.Add(lecture);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Lectures == null)
            {
                return NotFound();
            }

            var lecture = await db.Lectures.FindAsync(id);
            if (lecture == null)
            {
                return NotFound();
            }
            ViewBag.Courses = new SelectList(db.Courses, "Id", "Coursename");
            return View(lecture);
        }

        [HttpPost]
        public IActionResult Edit(Lecture lecture)
        {

            db.Lectures.Update(lecture);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            var lecture = db.Lectures.Find(id);
            if (lecture != null)
            {
                return View(lecture);
            }
            else
            {
                return RedirectToAction("Index"); // Ensure a valid action name here
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            // Find the instructor by the provided id
            var lecture = db.Lectures.Find(id);

            if (lecture != null)
            {
                // Remove the found instructor entity
                db.Lectures.Remove(lecture);

                // Save changes to the database
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
