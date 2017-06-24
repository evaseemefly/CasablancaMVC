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

        public PartialViewResult Menu(int selectedCategoryId)
        {
            var categories = _categoryService.Get();

            AutoMapper.Mapper.Initialize(c => c.CreateMap<Category, CategoryViewModel>());

            ViewBag.SelectedCategoryId = selectedCategoryId;

            return PartialView(AutoMapper.Mapper.Map<Category>())
        }
    }
}