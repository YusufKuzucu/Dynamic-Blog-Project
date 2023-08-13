using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
	public class RegisterController : Controller
	{
		WriterManager manager = new WriterManager(new EfWriterRepository());
		
        [HttpGet]
		public IActionResult Index()
		{

			return View();
		}

		[HttpPost]
		public IActionResult Index(Writer writer)
		{
			manager.WriterAdd(writer);	
			return RedirectToAction("Index");
		}
	}
}
