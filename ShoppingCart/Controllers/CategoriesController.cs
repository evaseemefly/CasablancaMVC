using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ShoppingCart.Models;
using ShoppingCart.ViewModels;
using ShoppingCart.DAL;
using ShoppingCart.Services;

namespace ShoppingCart.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryService _categoryService = new CategoryService();

        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult Menu(int selectedCategoryId)
        {
            var categories = _categoryService.Get();

            AutoMapper.Mapper.Initialize(c => c.CreateMap<Category, CategoryViewModel>());

            ViewBag.SelectedCategoryId = selectedCategoryId;

            var model = AutoMapper.Mapper.Map<List<Category>, List<CategoryViewModel>>(categories);
            return PartialView(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _categoryService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}