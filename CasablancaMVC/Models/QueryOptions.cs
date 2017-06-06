using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasablancaMVC.Models
{
    public class QueryOptions
    {
        public QueryOptions()
        {
            SortField = "Id";
            SortOrder = SortOrder.ASC;
            CurrentPage = 1;
            PageSize = 1;
        }

        /// <summary>
        /// 定义模型中的排序字段
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// 指定排序方向
        /// </summary>
        public SortOrder SortOrder { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public string Sort
        {
            get
            {
                return string.Format("{0} {1}", SortField, SortOrder.ToString());
            }
        }
    }
}