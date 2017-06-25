using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

using ShoppingCart.Models;
using ShoppingCart.Services;
using ShoppingCart.ViewModels;

namespace ShoppingCart.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService = new BookService();

        public BooksController()
        {
            Mapper.Initialize(c => c.CreateMap<Book, BookViewModel>());
            Mapper.Initialize(c => c.CreateMap<Author, AuthorViewModel>());
            Mapper.Initialize(c => c.CreateMap<Category, CategoryViewModel>());
        }
        // GET: Books
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Featured()
        {

        }
    }
}