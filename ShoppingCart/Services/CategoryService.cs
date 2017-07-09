﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Models;
using ShoppingCart.DAL;

namespace ShoppingCart.Services
{
    public class CategoryService : IDisposable
    {
        private ShoppingCartContext _db = new ShoppingCartContext();

        public List<Category> Get()
        {
            return _db.Categories.OrderBy(c => c.Name).ToList();
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
