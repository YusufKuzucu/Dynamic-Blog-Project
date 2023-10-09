using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCommentController : Controller
    {
        CommentManager manager = new CommentManager(new EfCommentRepository());
        public IActionResult Index()
        {
            var values = manager.GetCommentWithBlog();
            return View(values);
        }
    }
}
