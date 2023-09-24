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
            ViewBag.v1 =context.Blogs.Count().ToString();
            ViewBag.v2 =context.Blogs.Where(x=>x.WriterID==1).Count();
            ViewBag.v3 = context.Categories.Count();
            return View();
        }
    }
}
