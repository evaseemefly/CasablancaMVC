using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CasablancaMVC.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CasablancaMVC.DAL
{
    public class BookContext:DbContext
    {
        /// <summary>
        /// 调用基类的构造函数并传入BookContext字符串，
        /// DbContext将使用该字符串从web.config中获取连接字符串
        /// </summary>
        public BookContext() : base("BookContext")
        {

        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //使用System.Data.Entity.ModelConfiguration.Conventions 命名空间
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}