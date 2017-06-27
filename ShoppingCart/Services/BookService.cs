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

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
