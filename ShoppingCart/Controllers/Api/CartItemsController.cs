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
cfg.CreateMap<CartItem, CartItemViewModel>();
                    cfg.CreateMap<Book, BookViewModel>();
                    
                    cfg.CreateMap<CartItemViewModel, CartItem>();
                   // cfg.CreateMap<CartItemViewModel, CartItem>();
                    cfg.CreateMap<BookViewModel, Book>();
                    cfg.CreateMap<AuthorViewModel, Author>();
                    cfg.CreateMap<Author, AuthorViewModel>();
                    cfg.CreateMap<CategoryViewModel, Category>();
                    cfg.CreateMap<Category, CategoryViewModel>();
                });

                //AutoMapper.Mapper.CreateMap<Cart, CartViewModel>();
                //AutoMapper.Mapper.CreateMap<CartItem, CartItemViewModel>();
                //AutoMapper.Mapper.CreateMap<Book, BookViewModel>();

                //AutoMapper.Mapper.CreateMap<CartItemViewModel, CartItem>();
                //AutoMapper.Mapper.CreateMap<BookViewModel, Book>();
                //AutoMapper.Mapper.CreateMap<AuthorViewModel, Author>();
                //AutoMapper.Mapper.CreateMap<CategoryViewModel, Category>();

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
            var cartItem_temp = mapper.Map<CartItemViewModel, CartItem>(cartItem);
            var newCartItem = _cartItemService.Add2Cart(cartItem_temp);

            var temp = new CartItem()
            {
                //Book =mapper.Map<Book,BookViewModel>(newCartItem.Book),
                
                 Book=newCartItem.Book,
                BookId = newCartItem.BookId,
                //Cart =mapper.Map<Cart,CartViewModel>(newCartItem.Cart),
                //Cart=newCartItem.Cart,
                CartId = newCartItem.CartId,
                Id = newCartItem.Id,
                Quantity = newCartItem.Quantity

            };
            var book_vm = mapper.Map<Book, BookViewModel>(newCartItem.Book);
            var cartItemVM_temp = mapper.Map<CartItem, CartItemViewModel>(temp);
            return cartItemVM_temp;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
            
        }

        public CartItemViewModel Put(CartItemViewModel cartItem)
        {
            _cartItemService.UpdateCartItem(mapper.Map<CartItemViewModel, CartItem>(cartItem));

            return cartItem;
        }

        public CartItemViewModel Delete(CartItemViewModel cartItem)
        {
            _cartItemService.DeleteCartItem(mapper.Map<CartItemViewModel, CartItem>(cartItem));

            return cartItem;
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}