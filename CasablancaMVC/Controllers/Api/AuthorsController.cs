using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CasablancaMVC.DAL;
using CasablancaMVC.ViewModel;
using CasablancaMVC.Models;

namespace CasablancaMVC.Controllers.Api
{
    public class AuthorsController : ApiController
    {
        private BookContext db = new BookContext();

        public ResultList<AuthorViewModel> Get([FromUri]QueryOptions queryOptions)
        {
            var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;

            var author = db.Authors.
                OrderBy(queryOptions.Sort).
                Skip(start).
                Take(queryOptions.PageSize);

            queryOptions.TotalPages = (int)Math.Ceiling((double)db.Authors.Count() / queryOptions.PageSize);

            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<Author, AuthorViewModel>());

            return new ResultList<AuthorViewModel>
            {
                QueryOptions = queryOptions,
                Result = AutoMapper.Mapper.Map<List<Author>, List<AuthorViewModel>>(author.ToList())
            };

        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
