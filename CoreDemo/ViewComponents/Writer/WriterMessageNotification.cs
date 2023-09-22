using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Writer
{
    public class WriterMessageNotification:ViewComponent
    {
        Message2Manager manager = new Message2Manager(new EfMessage2Repository());
        public IViewComponentResult Invoke()
        {
            int p = 2;
            var values = manager.GetInboxListByWriter(p);
            return View(values);
        }
    }
}
