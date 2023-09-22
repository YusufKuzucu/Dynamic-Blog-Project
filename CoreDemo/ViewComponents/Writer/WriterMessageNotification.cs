using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Writer
{
    public class WriterMessageNotification:ViewComponent
    {
        MessageManager manager = new MessageManager(new EfMessageRepository());
        public IViewComponentResult Invoke()
        {
            string p = "deneme@gmail.com";
            var values = manager.GetInboxListByWriter(p);
            return View(values  );
        }
    }
}
