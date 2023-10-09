using CoreDemo.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        public AdminRoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppRole roole = new AppRole()
                {
                    Name = model.Name,
                };
                var result = await _roleManager.CreateAsync(roole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult UpdateRole(int id)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            RoleUpdateVM role = new RoleUpdateVM()
            {
                Id = values.Id,
                Name = values.Name
            };
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleUpdateVM model)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == model.Id);
            values.Name = model.Name;

            var result=await _roleManager.UpdateAsync(values);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var values= _roleManager.Roles.FirstOrDefault(y => y.Id == id);
            var result=await _roleManager.DeleteAsync(values);
            if (result.Succeeded)
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            return View();
        }
    }
}
