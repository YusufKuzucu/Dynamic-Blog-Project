using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreDemo.Controllers
{
    public class NotificationController : Controller
    {
        NotificationManager manager = new NotificationManager(new EfNotificationRepository());
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult AllNotification()
        {
            var values = manager.GetList();
            
            return View(values);
        }
    }
}
