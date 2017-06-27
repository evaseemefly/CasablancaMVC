using AutoMapper;
using My_AutoMapper.Models;
using My_AutoMapper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_AutoMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            //autoMapper测试
            Mapper.Initialize(c => c.CreateMap<Author, AuthorViewModel>());
            var author = new Author()
            {
                Biography = "123",
                FirstName = "22",
                Id = 1,
                LastName = "sdfsdf"
            };
            var cat = new Category()
            {
                Id = 1,
                Name = "测试"
            };

            var book_temp = new Book()
            {

                AuthorId = 1,
                CategoryId = 1,
                Description = "",
                Featured = true,
                Id = 1,
                ImageUrl = "",
                Isbn = "",
                ListPrice = 1,
                SalePrice = 1,
                Synopsis = "123",
                Title = "23",
                Author = author,
                Category = cat
            };

            var author_temp = Mapper.Map<Author, AuthorViewModel>(author);
            Mapper.Initialize(c => c.CreateMap<Book, BookViewModel>());

            //Mapper.Initialize(c => c.CreateMap<Category, CategoryViewModel>());
            var book_dto = Mapper.Map<Book, BookViewModel>(book_temp);
            
        }
    }
}
