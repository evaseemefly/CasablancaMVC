using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_AutoMapper.Models
{
    public class Book
    {
        
        public int Id { get; set; }

        
        public string Title { get; set; }

        
        public string Isbn { get; set; }

        /// <summary>
        /// 大纲
        /// </summary>
        
        public string Synopsis { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        
        public string Description { get; set; }

        
        public string ImageUrl { get; set; }

        
        public decimal ListPrice { get; set; }

      
        public decimal SalePrice { get; set; }

        /// <summary>
        /// 特定
        /// </summary>
       
        public bool Featured { get; set; }

        
        public int SavePercentage
        {
            get
            {
                return (int)(100 - (SalePrice / ListPrice * 100));
            }
        }

        public virtual Author Author { get; set; }

        /// <summary>
        /// 种类
        /// </summary>
        public virtual Category Category { get; set; }
        //public int Id { get; set; }

        //public int AuthorId { get; set; }

        //public int CategoryId { get; set; }

        //public string Title { get; set; }

        //public string Isbn { get; set; }

        ///// <summary>
        ///// 大纲
        ///// </summary>
        //public string Synopsis { get; set; }

        ///// <summary>
        ///// 描述
        ///// </summary>
        //public string Description { get; set; }

        //public string ImageUrl { get; set; }

        //public decimal ListPrice { get; set; }

        //public decimal SalePrice { get; set; }

        ///// <summary>
        ///// 特定
        ///// </summary>
        //public bool Featured { get; set; }




    }
}
