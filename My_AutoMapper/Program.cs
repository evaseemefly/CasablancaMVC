using AutoMapper;
using AutoMapper.Configuration;
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
           // Mapper.Initialize(c => c.CreateMap<Author, AuthorViewModel>());
            //Mapper.Initialize(c => c.CreateMap<Book, BookViewModel>());
            //var cfg_config=new IMapperConfigurationExpression()
            //var config = new MapperConfiguration(c=>c.CreateMap<

             //Mapper.Initialize(cfg => cfg.CreateMap<Author, AuthorViewModel>());
            //or
           

            var author = new Author()
            {
                Biography = "123",
                FirstName = "22",
                Id = 1,
                LastName = "sdfsdf"
            };
            var category = new Category()
            {
                Id = 1,
                Name = "测试"
            };

            var book = new Book()
            {

                //AuthorId = 1,
                //CategoryId = 1,
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
                Category = category
            };
            //初始化Mapper的方式一
            //使用静态Mapper进行初始化（Initialize）
            Mapper.Initialize(c => c.CreateMap<Book, BookViewModel>());
            Mapper.Initialize(c => c.CreateMap<Author, AuthorViewModel>());
            Mapper.Initialize(c => c.CreateMap<Category, CategoryViewModel>());
            var dto_author = Mapper.Map<Author, AuthorViewModel>(author);
            //使用此种方式会报错
            var dto_book = Mapper.Map<Book, BookViewModel>(book);

            //初始化Mapper的方式二
            //实例化MapperConfiguration的方式
            var config_test = new MapperConfiguration(cfg => {
                cfg.CreateMap<Author, AuthorViewModel>();
                cfg.CreateMap<Book, BookViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>();
            });
            var mapper = config_test.CreateMapper();
            var dto_author_byInstance = mapper.Map<Author, AuthorViewModel>(author);
            var dto_book_byInstance = mapper.Map<Book, BookViewModel>(book);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookViewModel>());
            

            var cfxE = new MapperConfigurationExpression();
            cfxE.CreateMap<Author, AuthorViewModel>();
            cfxE.CreateMap<Book, BookViewModel>();
            cfxE.CreateMap<Category, CategoryViewModel>();
            Mapper.Initialize(cfxE);
            //var dto_book = mapper.Map<Book, BookViewModel>(book_temp);




            //Mapper.Initialize(c => c.CreateMap<Category, CategoryViewModel>());
            var book_dto = Mapper.Map<Book, BookViewModel>(book);
            
        }
    }
}
