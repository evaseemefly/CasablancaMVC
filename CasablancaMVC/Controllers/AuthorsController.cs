using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasablancaMVC.DAL;
using System.Web.ModelBinding;

using CasablancaMVC.Models;
using CasablancaMVC.ViewModel;
using CasablancaMVC.Filters;
using CasablancaMVC.Behaviors;
using CasablancaMVC.Services;

namespace CasablancaMVC.Controllers
{
    public class AuthorsController : Controller
    {
        //
        //private BookContext db = new BookContext();

        //GET: Authors
        //public ActionResult Index()
        //{
        //    return View(db.Authors.ToList());
        //}

        /// <summary>
        /// 
        /// </summary>
        private AuthorService authorService;

        /// <summary>
        /// 
        /// </summary>
        public AuthorsController()
        {
            authorService = new AuthorService();
            AutoMapper.Mapper.Initialize(c => c.CreateMap<Author, AuthorViewModel>());
        }

        public ActionResult TestIndex()
        {
            return View();
        }

        [GenerateResultListFilter(typeof(Author),typeof(AuthorViewModel))]
        public ActionResult Index([Form]QueryOptions queryOptions)
        {
            #region 
            /*
                1 首次打开页面时：
                queryOptions
                {
                    "SortField": "Id",
                    "SortOrder": 0,
                    "Sort": "Id ASC"
                }
                2 将firstName按照升序排列后：
                {
  "SortField": "FirstName",
  "SortOrder": 0,
  "Sort": "FirstName ASC"
}
            3 将firstName按照降序排列后：
            {
  "SortField": "FirstName",
  "SortOrder": 1,
  "Sort": "FirstName DESC"
}
            */
            #endregion

            var authors = authorService.Get(queryOptions);

            #region 使用胖模型、瘦控制器时将之前的方式注释掉
            //var list_temp= db.Authors.ToList();
            //var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;

            //var authors = db.Authors.
            //    OrderBy(queryOptions.Sort).
            //    Skip(start).
            //    Take(queryOptions.PageSize);

            //queryOptions.TotalPages = (int)Math.Ceiling((double)db.Authors.Count() / queryOptions.PageSize);
            //新版本的autoMapper初始化起改为使用以下方式初始化
            // AutoMapper.Mapper.Initialize(cfg=>cfg.CreateMap<Author,AuthorViewModel>());
            //var list= AutoMapper.Mapper.Map<List<Author>, List<AuthorViewModel>>(authors.ToList());
            #endregion


            ViewBag.QueryOptions = queryOptions;
           

            ViewData["QueryOptions"] = queryOptions;
            return View(authors);
            //return View(new ResultList<AuthorViewModel>
            //{
            //    QueryOptions = queryOptions,
            //    Result = AutoMapper.Mapper.Map<List<Author>, List<AuthorViewModel>>(authors.ToList())
            //});
            //return View(list);
            //return View(authors.ToList());
        }


        // GET: Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var author = authorService.GetById(id.Value);
            #region 使用胖模型、瘦控制器时将之前的方式注释掉
            //Author author = db.Authors.Find(id);
            //if (author == null)
            //{
            //    throw new System.Data.Entity.Core.ObjectNotFoundException(string.Format("找不到指定作者，id为{0}", id));
            //    //return HttpNotFound();
            //}
            //return View(author);
            #endregion
            var model = AutoMapper.Mapper.Map<Author, AuthorViewModel>(author);
            return View("Form", model);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            //return View();
            //return View("Form", new Author());
            return View("Form", new AuthorViewModel());
        }
        #region 暂时注释掉创建——Create([Bind(Include = "Id,FirstName,LastName,Biography")] AuthorViewModel author)
        //// POST: Authors/Create
        //// 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        //// 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Biography")] AuthorViewModel/*Author*/ author)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //初始化autoMapper
        //        AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<AuthorViewModel, Author>());
        //        db.Authors.Add(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author));
        //       // db.Authors.Add(author);
        //       int result= db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(author);
        //}
        #endregion


        // GET: Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var author = authorService.GetById(id.Value);
           // Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }

            //AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<Author, AuthorViewModel>());
            //return View(author);
            return View("Form", AutoMapper.Mapper.Map<Author,AuthorViewModel >(author)/*author*/);
        }

        #region 暂时注释掉——Edit([Bind(Include = "Id,FirstName,LastName,Biography")] AuthorViewModel author)
        //// POST: Authors/Edit/5
        //// 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        //// 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Biography")] AuthorViewModel/*Author*/ author)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<AuthorViewModel, Author>());
        //        db.Entry(AutoMapper.Mapper.Map<AuthorViewModel,Author>(author)/*author*/).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(author);
        //}
        #endregion


        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var author = authorService.GetById(id.Value);
            //Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            //AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<Author, AuthorViewModel>());

            return View(AutoMapper.Mapper.Map<Author,AuthorViewModel>(author)/*author*/);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var author = authorService.GetById(id);
            authorService.Delete(author);
            //Author author = db.Authors.Find(id);
            //db.Authors.Remove(author);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                authorService.Dispose();
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
