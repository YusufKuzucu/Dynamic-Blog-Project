using CoreDemo.Models;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(UserSignViewModel p)
		{
			if (ModelState.IsValid)
			{
                var result = await _signInManager.PasswordSignInAsync(p.UserName, p.Password, false, true);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Dasboard");
				}
				else
				{
                    return RedirectToAction("Index", "Login");
                }
            }
			return View(p);
        }





		//[HttpPost]
		//public async Task<IActionResult> Index(Writer p)
		//{
		//	Context c=new Context();
		//	var datavalue = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
		//	if (datavalue != null)
		//	{
		//		var claims = new List<Claim>
		//		{
		//			new Claim(ClaimTypes.Name,p.WriterMail)
		//		};
		//		var userIdentity = new ClaimsIdentity(claims,"a");
		//		ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
		//		await HttpContext.SignInAsync(userPrincipal);

		//		return RedirectToAction("Index","Dasboard");
		//	}
		//	else
		//	{
		//		return View();

		//	}

		//}
	}
}

//Context c = new Context();
//var dataValue = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
//if (dataValue != null)
//{
//	HttpContext.Session.SetString("username", p.WriterMail);
//	return RedirectToAction("Index", "Writer");
//}
//else
//{
//	return View();

//}
