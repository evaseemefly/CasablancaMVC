using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ShoppingCart.Models;

namespace ShoppingCart.DAL
{
    public class DataInitialization:DropCreateDatabaseIfModelChanges<ShoppingCartContext>
    {
        protected override void Seed(ShoppingCartContext context)
        {
            var categories = new List<Category>
            {
                new Category
                {
                     Name="科技"
                },
                new Category
                {
                     Name="科幻小说"
                },
                new Category
                {
                    Name="非虚构的"
                },
                new Category
                {
                    Name="漫画"
                }
            };

            //将种类加入上下文对象中
            categories.ForEach(c => context.Categories.Add(c));

            var author = new Author
            {
                Biography = "...",
                FirstName = "dr",
                LastName = "no"
            };

            var books = new List<Book>
            {
                new Book
                {
                    Author=author,
                    Category=categories[0],
                    Description="...",
                    Featured=true,
                     ImageUrl="...",
                     ListPrice=19.99m,
                     SalePrice=17.99m,
                     Synopsis="...",
                     Title="追风筝的人"
                },
                 new Book
                {
                    Author=author,
                    Category=categories[0],
                    Description="...",
                    Featured=false,
                     ImageUrl="...",
                     ListPrice=14.9m,
                     SalePrice=13.9m,
                     Synopsis="...",
                     Title="梦里花落知多少"
                },
                  new Book
                {
                    Author=author,
                    Category=categories[0],
                    Description="...",
                    Featured=true,
                     ImageUrl="...",
                     ListPrice=19.99m,
                     SalePrice=17.99m,
                     Synopsis="...",
                     Title="岛上书店"
                }
            };
            books.ForEach(b => context.Books.Add(b));
            context.SaveChanges();
            //base.Seed(context);
        }
    }
}
