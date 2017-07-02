using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ShoppingCart.DAL;
using ShoppingCart.Models;
using AutoMapper;
using ShoppingCart.ViewModels;

namespace ShoppingCart.Services
{
    public class CartService:IDisposable
    {
        
        //数据上下文对象——此处可继续修改为单例模式创建（或线程中唯一）
        private ShoppingCartContext _db = new ShoppingCartContext();

        public IMapper mapper = null;

        public CartService()
        {
            if (mapper == null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Author, AuthorViewModel>();
                    cfg.CreateMap<Book, BookViewModel>();
                    cfg.CreateMap<Category, CategoryViewModel>();

                    cfg.CreateMap<CartItemViewModel, CartItem>();
                    cfg.CreateMap<BookViewModel, Book>();
                    cfg.CreateMap<AuthorViewModel, Author>();
                    cfg.CreateMap<CategoryViewModel, Category>();
                });
                mapper = config.CreateMapper();
            }
        }

        public Cart GetBySessionId(string sessionId)
        {
            //Include的意义
            var cart = _db.Carts.
                Include("CartItems").//返回Carts中的CartItems集合
                Where(c => c.SessionId == sessionId).
                SingleOrDefault();

            cart = CreateCartIfItDoesntExist(sessionId, cart);

            return cart;
        }

        /// <summary>
        /// 根据sessionId创建Cart对象，并返回
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        public Cart CreateCartIfItDoesntExist(string sessionId,Cart cart)
        {
            //此时cart不使用ref与out的原因
            if (null == cart)
            {
                //注意此处创建的购物车中保存了SessionId（可使用memcache单独保存缓存）
                cart = new Cart
                {
                    SessionId = sessionId,
                    CartItems = new List<CartItem>()
                };
                _db.Carts.Add(cart);
                _db.SaveChanges();
            }
            return cart;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        
    }
}
