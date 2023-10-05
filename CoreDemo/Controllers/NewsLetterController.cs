using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace CoreDemo.Controllers
{
	[AllowAnonymous]
	public class NewsLetterController : Controller
	{
		NewsLetterManager nl = new NewsLetterManager(new EfNewsLetterRepository());
		[HttpGet]
		public PartialViewResult SubscribeMail()
		{
			return PartialView();
		}
		[HttpPost]
		public IActionResult SubscribeMail(NewsLetter p)
		{
			p.MailStatus = true;
			nl.AddNewsLetter(p);
			return PartialView();
		}

	}
}
