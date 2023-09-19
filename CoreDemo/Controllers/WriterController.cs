using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace CoreDemo.Controllers
{
	public class WriterController : Controller
	{
		WriterManager manager = new WriterManager(new EfWriterRepository());
		public IActionResult Index()
		{
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
		[AllowAnonymous]
		[HttpGet]
		public IActionResult WriterEditProfile()
		{
			var writerValues = manager.GetById(1);
			return View(writerValues);
		}
		[AllowAnonymous]	
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
