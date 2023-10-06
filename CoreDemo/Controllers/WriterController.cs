using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
	public class WriterController : Controller
	{
		WriterManager manager = new WriterManager(new EfWriterRepository());
		private readonly UserManager<AppUser> _userManager;

        public WriterController( UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
		public IActionResult Index()
		{
			var usermail = User.Identity.Name;
			ViewBag.v = usermail;
			Context c=new Context();
			var writer = c.Writers.Where(x => x.WriterMail == usermail).Select(x => x.WriterName).FirstOrDefault();
			ViewBag.v2 = writer;
			return View();
		}
		[AllowAnonymous]
		public IActionResult Test()
		{
			return View();
		}
		[AllowAnonymous]
		public PartialViewResult WriterNavbarPartial()
		{
			return PartialView();
		}
		[AllowAnonymous]
		public PartialViewResult WriterFooterPartial()
		{
			return PartialView();
		}
		[HttpGet]
		public IActionResult WriterEditProfile()
		{
			var userName = User.Identity.Name;
			Context c = new Context();
			var usermail = c.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
			UserManager userManager = new UserManager(new EfUserRepository());
			//         var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(x => x.WriterID).FirstOrDefault();
			//         var writerValues = manager.GetById(writerID);
			//return View(writerValues);
			var id = c.Users.Where(x => x.Email == usermail).Select(x=>x.Id).FirstOrDefault();
			var values = userManager.GetById(id);
			return View(values);
		}
        [HttpPost]		
        public IActionResult WriterEditProfile(Writer p)
        {
			WriterValidator wl=new WriterValidator();
			var result=wl.Validate(p);
			if (result.IsValid)
			{
                manager.TUpdate(p);

				return RedirectToAction("Index", "Dasboard");

            }
			else
			{
                foreach (var item in result.Errors)
                {
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
		[AllowAnonymous]
		[HttpGet]
		public IActionResult WriterAdd()
		{
			return View();
		}
        [AllowAnonymous]
        [HttpPost]
        public IActionResult WriterAdd(AddProfileImage p)
        {
			Writer w = new Writer();
			if (p.WriterImage != null)
			{
				var extension = Path.GetExtension(p.WriterImage.FileName);
				var newImageName = Guid.NewGuid() + extension;
				var location=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/",newImageName);
				var stream = new FileStream(location, FileMode.Create);
				p.WriterImage.CopyTo(stream);
				w.WriterImage = newImageName;

            }
			w.WriterMail = p.WriterMail;
			w.WriterPassword = p.WriterPassword;
			w.WriterStatus = true;
			w.WriterName = p.WriterName;
			w.WriterAbout = p.WriterAbout;
			manager.TAdd(w);
            return RedirectToAction("Index", "Dasboard");

        }
    }
}
