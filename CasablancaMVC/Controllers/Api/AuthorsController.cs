using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using System.Web.Http.Description;

using CasablancaMVC.DAL;
using CasablancaMVC.ViewModel;
using CasablancaMVC.Models;
using CasablancaMVC.Services;

namespace CasablancaMVC.Controllers.Api
{
    public class AuthorsController : ApiController
    {
        private AuthorService authorService;

        public AuthorsController()
        {
            authorService = new AuthorService();
            AutoMapper.Mapper.Initialize(c => c.CreateMap<Author, AuthorViewModel>());
            AutoMapper.Mapper.Initialize(c => c.CreateMap<AuthorViewModel, Author>());
        }

        //private BookContext db = new BookContext();

        public ResultList<AuthorViewModel> Get([FromUri]QueryOptions queryOptions)
        {
           var author= authorService.Get(queryOptions);

            #region 使用胖模型，瘦控制器之后注释掉此部分
            //var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;

            //var author = db.Authors.
            //    OrderBy(queryOptions.Sort).
            //    Skip(start).
            //    Take(queryOptions.PageSize);

            //queryOptions.TotalPages = (int)Math.Ceiling((double)db.Authors.Count() / queryOptions.PageSize);
            #endregion


            //AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<Author, AuthorViewModel>());

            //return new ResultList<AuthorViewModel>
            //{
            //    QueryOptions = queryOptions,
            //    Result = AutoMapper.Mapper.Map<List<Author>, List<AuthorViewModel>>(author.ToList())
            //};
            return new ResultList<AuthorViewModel>(AutoMapper.Mapper.Map<List<Author>, List<AuthorViewModel>>(author.ToList()), queryOptions);
        }

        public IHttpActionResult Get(int id)
        {
           var author= authorService.GetById(id);
            //Author author = db.Authors.Find(id);
            if (author == null)
            {
                throw new System.Data.Entity.Core.ObjectNotFoundException(string.Format("未找到指定作者{0}", id));
            }

            //AutoMapper.Mapper.Initialize(c => c.CreateMap<Author, AuthorViewModel>());

            return Ok(AutoMapper.Mapper.Map<Author, AuthorViewModel>(author));
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(AuthorViewModel author)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //AutoMapper.Mapper.Initialize(c => c.CreateMap<AuthorViewModel, Author>());
            var model = AutoMapper.Mapper.Map<AuthorViewModel, Author>(author);

            authorService.Update(model);
            //db.Entry(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author)).State = EntityState.Modified;

            //db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(AuthorViewModel))]
        public IHttpActionResult Post(AuthorViewModel author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = AutoMapper.Mapper.Map<AuthorViewModel, Author>(author);
            authorService.Insert(model);
            //AutoMapper.Mapper.Initialize(c => c.CreateMap<AuthorViewModel, Author>());
            //db.Authors.Add(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author));
            
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = author.Id }, author);
            //return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Author))]
        public IHttpActionResult DeleteAuthor(int id)
        {
            var author = authorService.GetById(id);
            authorService.Delete(author);
            return Ok(author);
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                // db.Dispose();
                authorService.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
