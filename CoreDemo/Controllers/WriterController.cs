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
		public async  Task<IActionResult> WriterEditProfile()
		{
			var values = await _userManager.FindByNameAsync(User.Identity.Name);
			UserUpdateViewModel model=new UserUpdateViewModel();
            model.nameSurname = values.UserName;
            model.mail = values.Email;
            model.imageurl = values.ImageUrl;	
            model.username = values.UserName;
            return View(model);
		}
        [HttpPost]		
        public async Task<IActionResult> WriterEditProfile(UserUpdateViewModel model)
        {
			var values = await _userManager.FindByNameAsync(User.Identity.Name);
			model = new UserUpdateViewModel();
			values.NameSurname = model.nameSurname;
			values.ImageUrl = model.imageurl;
			values.Email = model.mail;
			values.UserName=model.username;
			values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, model.password);
			var result=await _userManager.UpdateAsync(values);	
            return RedirectToAction("Index", "Dasboard");
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
