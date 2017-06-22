using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ShoppingCart.DAL;
using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public class CartService:IDisposable
    {
        //数据上下文对象——此处可继续修改为单例模式创建（或线程中唯一）
        private ShoppingCartContext _db = new ShoppingCartContext();

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
            //此时cart不适用ref与out的原因
            if (null == cart)
            {
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
