using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ShoppingCart.Models;
using ShoppingCart.Services;
using ShoppingCart.ViewModels;
using AutoMapper;

namespace ShoppingCart.Controllers.Api
{
    public class CartItemsController : ApiController
    {
        private readonly CartItemService _cartItemService = new CartItemService();

        public IMapper mapper = null;

        public CartItemsController()
        {
            if (mapper == null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Cart, CartViewModel>();
                    cfg.CreateMap<Book, BookViewModel>();
                    cfg.CreateMap<CartItem, CartItemViewModel>();

                    cfg.CreateMap<CartItemViewModel, CartItem>();
                    cfg.CreateMap<BookViewModel, Book>();
                    cfg.CreateMap<AuthorViewModel, Author>();
                    cfg.CreateMap<CategoryViewModel, Category>();
                });
                mapper = config.CreateMapper();
            }
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public CartItemViewModel Post(CartItemViewModel cartItem)
        {
            //注意此处的AutoMapper实现了双向映射
            //因为传入的CartItemViewModel——>CartItem
            //CartItem——>CartItemViewModel
            var newCartItem = _cartItemService.Add2Cart(mapper.Map < CartItemViewModel, CartItem>(cartItem));
            return mapper.Map<CartItem, CartItemViewModel>(newCartItem);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}