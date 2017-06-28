using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShoppingCart.DAL;
using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public class BookService : IDisposable
    {
        private ShoppingCartContext _db = new ShoppingCartContext();

        /// <summary>
        /// 找到喜欢的图书
        /// </summary>
        /// <returns></returns>
        public List<Book> GetFeatured()
        {
            return _db.Books.
                Include("Author").
                Where(b => b.Featured).
                ToList();
        }

        public Book GetById(int id)
        {
            var book = _db.Books.
                Include("Author").
                Where(b => b.Id == id).
                SingleOrDefault();
            if (null == book)
            {
                throw new System.Data.Entity.Core.ObjectNotFoundException(string.Format("无法找到对应书籍{0}", id));
            }

            return book;
        }

        public List<Book> GetByCategoryId(int categoryId)
        {
            var list = _db.Books.
                Include("Author").
                Where(b => b.CategoryId == categoryId).
                OrderByDescending(b => b.Featured).
                ToList();
            return list;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
