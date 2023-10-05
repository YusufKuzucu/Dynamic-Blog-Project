using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.ViewComponents.Writer
{
    public class WriterAboutOnDashborad:ViewComponent
    {
        WriterManager wm=new WriterManager(new EfWriterRepository());
        public IViewComponentResult Invoke()
        {
            var userName = User.Identity.Name;
            ViewBag.v = userName;
            Context c = new Context();
            var userMail = c.Users.Where(x => x.UserName == userName).Select(x => x.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == userMail).Select(x => x.WriterID).FirstOrDefault();
            var values = wm.GetWriterByID(writerID);
            return View(values);
        }
    }
}
