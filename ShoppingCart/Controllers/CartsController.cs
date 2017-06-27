using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Services;
using ShoppingCart.ViewModels;
using AutoMapper;

namespace ShoppingCart.Controllers
{
    public class CartsController : Controller
    {
        private readonly CartService _cartService = new CartService();

        static CartsController()
        {
            //创建实体=>ViewModel的映射关系（初始化）
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Cart, CartViewModel>());
            //var mapper = config.CreateMapper();
            //使用此种方式也不行
            //new MapperConfiguration(c => c.CreateMap<Cart, CartViewModel>()).CreateMapper();
            //若在构造函数中初始化AutoMapper，在action中会出现如下错误:
            //Missing type map configuration or unsupported mapping
            AutoMapper.Mapper.Initialize(c => c.CreateMap<Cart, CartViewModel>());
            AutoMapper.Mapper.Initialize(c => c.CreateMap<CartItem, CartItemViewModel>());
            AutoMapper.Mapper.Initialize(c => c.CreateMap<Book, BookViewModel>());
            AutoMapper.Mapper.Initialize(c => c.CreateMap<Category, CategoryViewModel>());


        }

        [ChildActionOnly]
        public PartialViewResult Summary()
        {
            var cart = _cartService.GetBySessionId(HttpContext.Session.SessionID);
            //放在构造函数中会出问题
            AutoMapper.Mapper.Initialize(c => c.CreateMap<Cart, CartViewModel>());
            // AutoMapper.Mapper.DynamicMap<Cart, CartViewModel>(cart);
            //Mapper.Initialize(cfg => cfg.CreateMap<Cart, CartViewModel>());
            var dto = Mapper.Map<Cart,CartViewModel>(cart);
            #region 暂时注释掉
            //or
            // var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDto>());
            //var mapper = config.CreateMapper();
            //// or
            //var mapper = new Mapper(config);
            //OrderDto dto = mapper.Map<OrderDto>(order);
            // or
            //以以下这种方式会有错
            //var model = AutoMapper.Mapper.Map<Cart, CartViewModel>(cart);

            #endregion
            return PartialView(dto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cartService.Dispose();
            }

            base.Dispose(disposing);
        }

        // GET: Carts
        public ActionResult Index()
        {
            return View();
        }
    }
}