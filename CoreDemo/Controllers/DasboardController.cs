using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreDemo.Controllers
{
    public class DasboardController : Controller
    {
       
        Context context = new Context();
        public IActionResult Index()
        {
            var userName = User.Identity.Name;
            Context c = new Context();
            var userMail = c.Users.Where(x => x.UserName == userName).Select(x => x.Email).FirstOrDefault();
            var writerId=c.Writers.Where(x=>x.WriterMail==userMail).Select(y=>y.WriterID).FirstOrDefault();

            ViewBag.v1 =context.Blogs.Count().ToString();
            ViewBag.v2 =context.Blogs.Where(x=>x.WriterID==writerId).Count();
            ViewBag.v3 = context.Categories.Count();
            return View();
        }
    }
}
