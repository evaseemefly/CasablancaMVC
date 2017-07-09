using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CasablancaMVC.Behaviors;
using CasablancaMVC.DAL;
using CasablancaMVC.Models;
using CasablancaMVC.ViewModel;
using System.Linq.Dynamic;

namespace CasablancaMVC.Services
{
    public class AuthorService:IDisposable
    {
        private BookContext db = new BookContext();

        public void Dispose()
        {
            db.Dispose();
        }

        /// <summary>
        /// 根据指定id找到对应的作者对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Author GetById(long id)
        {
            Author author = db.Authors.Find(id);
            //作者不存在则抛出异常
            if (author == null)
            {
                throw new System.Data.Entity.Core.ObjectNotFoundException(string.Format("未找到指定id:{0}的作者", id));
            }
            return author;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="author"></param>
        public void Insert(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="author"></param>
        public void Update(Author author)
        {
            db.Entry(author).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="author"></param>
        public void Delete(Author author)
        {
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public List<Author> Get(QueryOptions queryOptions)
        {
            //1 获得起始页码
            var start = QueryOptionsCalculator.CalculateStart(queryOptions);
            //2 获得作者集合
            var authors = db.Authors.
                OrderBy(queryOptions.Sort).
                Skip(start).
                Take(queryOptions.PageSize);
            //3 获得总页码
            queryOptions.TotalPages = QueryOptionsCalculator.CalculateTotalPages(db.Authors.Count(), queryOptions.PageSize);

            return authors.ToList();

        }
    }
}
