﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasablancaMVC.DAL;
using CasablancaMVC.Models;
using System.Web.ModelBinding;

namespace CasablancaMVC.Controllers
{
    public class AuthorsController : Controller
    {
        private BookContext db = new BookContext();

        //GET: Authors
        //public ActionResult Index()
        //{
        //    return View(db.Authors.ToList());
        //}

        public ActionResult TestIndex()
        {
            return View();
        }

        public ActionResult Index([Form]QueryOptions queryOptions)
        {
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
            //var list_temp= db.Authors.ToList();
            var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;

            var authors = db.Authors.
                OrderBy(queryOptions.Sort).
                Skip(start).
                Take(queryOptions.PageSize);

            queryOptions.TotalPages = (int)Math.Ceiling((double)db.Authors.Count() / queryOptions.PageSize);

            ViewBag.QueryOptions = queryOptions;

            return View(authors.ToList());
        }


        // GET: Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            //return View();
            return View("Form", new Author());
        }

        // POST: Authors/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Biography")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
               int result= db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            //return View(author);
            return View("Form", author);
        }

        // POST: Authors/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Biography")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
