using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CasablancaMVC.Models;

namespace CasablancaMVC.DAL
{
    public class BookContext:DbContext
    {
        public BookContext() : base("BookContext")
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}