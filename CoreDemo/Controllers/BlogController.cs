using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());

        public IActionResult Index()
        {
            var values = bm.GetBlogListWithCategory();
            return View(values);
        }
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i= id;
            var values=bm.GetBlogByID(id);
            return View(values);
        }
        public IActionResult BlogByWriterList()
        {
            var values=bm.GetListCategoryWriterBm(1);
            return View(values);
        }
        [HttpGet]
        public IActionResult BlogAdd()
        {
            List<SelectListItem> categoryValues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.categoryValues = categoryValues;
            return View();
        }
        [HttpPost]
        public IActionResult BlogAdd(Blog p)
        {
            BlogValidator wv = new BlogValidator();
            ValidationResult result = wv.Validate(p);
            if (result.IsValid)
            {
                p.BlogStatus = true;
                p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.WriterID= 1;
                bm.TAdd(p);
                return RedirectToAction("BlogByWriterList", "Blog");
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

        public IActionResult DeleteBlog(int id)
        {
            var blogValue=bm.GetById(id);
            bm.TDelete(blogValue);
            return RedirectToAction("BlogByWriterList", "Blog");
        }
        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var values=bm.GetById(id);
            List<SelectListItem> categoryValues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.categoryValues = categoryValues;
            return View(values);
        }
        [HttpPost]
        public IActionResult EditBlog(Blog blog)
        {
            var blogValue = bm.GetById(blog.BlogID);
            blog.WriterID = 1;
            blog.BlogCreateDate = DateTime.Parse(blogValue.BlogCreateDate.ToShortDateString());
            blog.BlogStatus = true;
            bm.TUpdate(blog);
            return RedirectToAction("BlogByWriterList");
        }
    }
}
