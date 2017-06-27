﻿using System;
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
           // Mapper.Initialize(c => c.CreateMap<Book, BookViewModel>());
           
           
        }
        // GET: Books
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取喜欢的图书列表
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult Featured()
        {
            var books = _bookService.GetFeatured();

            
            Mapper.Initialize(c => c.CreateMap<Author, AuthorViewModel>());
            var author = new Author()
            {
                Biography = "123",
                FirstName = "22",
                Id = 1,
                LastName = "sdfsdf"
            };
            var cat = new Category()
            {
                Id = 1,
                Name = "测试"
            };

            var book_temp = new Book()
            {

                AuthorId = 1,
                CategoryId = 1,
                Description = "",
                Featured = true,
                Id = 1,
                ImageUrl = "",
                Isbn = "",
                ListPrice = 1,
                SalePrice = 1,
                Synopsis = "123",
                Title = "23",
                Author = author,
                Category = cat
            };

            var author_temp= Mapper.Map<Author, AuthorViewModel>(author);
            Mapper.Initialize(c => c.CreateMap<Book, BookViewModel>());
            
            //Mapper.Initialize(c => c.CreateMap<Category, CategoryViewModel>());
            var book_dto = Mapper.Map<Book, BookViewModel>(book_temp);
            var model = AutoMapper.Mapper.Map<List<Book>, List<BookViewModel>>(books);



            return PartialView(model);
        }

        /// <summary>
        /// 释放由 System.Web.Mvc.Controller 类的当前实例所使用的所有资源。
        /// Controller控制器基类中有该方法的虚方法
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._bookService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}