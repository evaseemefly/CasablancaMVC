using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CasablancaMVC.Models;

namespace CasablancaMVC.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class BookInitializer:DropCreateDatabaseIfModelChanges<BookContext>
    {
        protected override void Seed(BookContext context)
        {
            var author = new Author
            {
                Biography = "...",
                FirstName = "Dr",
                LastName = "No"
            };
            var books = new List<Book>
            {
                new Book
                {
                    Author=author,
                    Description="...",
                     ImageUrl="",
                     Isbn="123456",
                     Synopsis="...",
                     Title="测试书籍1"
                },
                new Book
                {
                    Author=author,
                    Description="...",
                     ImageUrl="",
                     Isbn="123456",
                     Synopsis="...",
                     Title="测试书籍1"
                },
                new Book
                {
                    Author=author,
                    Description="...",
                     ImageUrl="",
                     Isbn="112345",
                     Synopsis="...",
                     Title="测试书籍2"
                },
                new Book
                {
                    Author=author,
                    Description="...",
                     ImageUrl="",
                     Isbn="122345",
                     Synopsis="...",
                     Title="测试书籍3"
                }
            };
            books.ForEach(b => context.Books.Add(b));
            context.SaveChanges();
            //base.Seed(context);
        }
    }
}