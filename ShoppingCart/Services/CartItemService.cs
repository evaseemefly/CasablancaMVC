using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ShoppingCart.DAL;
using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public class CartItemService : IDisposable
    {
        private ShoppingCartContext _db = new ShoppingCartContext();

        public CartItem GetByCartIdAndBookId(int cartId,int bookId)
        {
            return _db.CartItem.SingleOrDefault(c => c.CartId == cartId && c.BookId == bookId);
        }

        public CartItem Add2Cart(CartItem cartItem)
        {
            var existingCartItem = GetByCartIdAndBookId(cartItem.CartId, cartItem.BookId);

            if (existingCartItem == null)
            {
                _db.Entry(cartItem).State = EntityState.Added;
                existingCartItem = cartItem;
            }
            else
            {
                existingCartItem.Quantity += cartItem.Quantity;
            }
            _db.SaveChanges();
            return existingCartItem;
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            _db.Entry(cartItem).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public void DeleteCartItem(CartItem cartItem)
        {
            _db.Entry(cartItem).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}