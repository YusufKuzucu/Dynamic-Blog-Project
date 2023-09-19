using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NotificationManager : INotificationService
    {
        INotificationDal _notification;
        public NotificationManager(INotificationDal notificationDal)
        {
            _notification = notificationDal;
        }

        public Notification GetById(int id)
        {
            return _notification.GetByID(id);
        }

        public List<Notification> GetList()
        {
            return _notification.GetListAll();
        }

        public void TAdd(Notification t)
        {
           _notification.Insert(t);
        }

        public void TDelete(Notification t)
        {
            _notification.Delete(t);
        }

        public void TUpdate(Notification t)
        {
            _notification.Update(t);
        }
    }
}
