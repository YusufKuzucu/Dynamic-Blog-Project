using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        public IActionResult Index(int page=1)
        {
            var values=categoryManager.GetList().ToPagedList(page,3);
            return View(values);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category p) 
        {   

            CategoryValidator wv = new CategoryValidator();
            ValidationResult result = wv.Validate(p);
            if (result.IsValid)
            {
                p.CategoryStatus = true;
                categoryManager.TAdd(p);
                return RedirectToAction("Index", "Category");
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
        public IActionResult CategoryDelete(int id)
        {
            //seewt alert kullanımı
            var blogValue = categoryManager.GetById(id);
            categoryManager.TDelete(blogValue);
            return RedirectToAction("Index");
        }
    }
}
