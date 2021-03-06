﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasablancaMVC.Models
{
    /// <summary>
    /// 查询对象
    /// </summary>
    public class QueryOptions
    {
        public QueryOptions()
        {
            SortField = "Id";
            SortOrder = SortOrder.ASC;
            CurrentPage = 1;
            PageSize = 10;
        }

        /// <summary>
        /// 定义模型中的排序字段
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// 指定排序方向
        /// </summary>
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 页容量
        /// </summary>
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