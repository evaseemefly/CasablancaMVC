using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ShoppingCart.Models;

namespace ShoppingCart.DAL
{
    /// <summary>
    /// 购物车数据上下文对象
    /// </summary>
    public class ShoppingCartContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItem { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //删除将表名称设置为实体类型名的复数版本的约束
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
