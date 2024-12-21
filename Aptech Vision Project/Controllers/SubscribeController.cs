using Aptech_Vision_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aptech_Vision_Project.Controllers
{
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    [Authorize(Roles = "admin")]
    public class SubscribeController : Controller
    {
        private readonly AptechVisionProjectContext db;

        public SubscribeController(AptechVisionProjectContext _db)
        {
            db = _db;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Subscribed(Subscriber subscriber)
        {
                        
                var checkEmail = db.Subscribers.FirstOrDefault(a => a.Subscribedemail == subscriber.Subscribedemail);
                if (checkEmail == null)
                {
                    db.Subscribers.Add(subscriber);
                    db.SaveChanges();
                    TempData["msg"] = "Successfully Subscribed!";
                }
                else
                {
                    TempData["msg"] = "Already Subscribed!";
                }
            

            return RedirectToAction("Index", "Home");
    }


}
}
