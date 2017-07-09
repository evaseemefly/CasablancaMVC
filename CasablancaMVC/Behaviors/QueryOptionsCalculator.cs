using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CasablancaMVC.ViewModel;
using CasablancaMVC.Models;

namespace CasablancaMVC.Behaviors
{
    public class QueryOptionsCalculator
    {
        /// <summary>
        /// 计算起始页码
        /// </summary>
        /// <param name="queryOptions"></param>
        /// <returns></returns>
        public static int CalculateStart(QueryOptions queryOptions)
        {
            return (queryOptions.CurrentPage - 1) * queryOptions.PageSize;
        }

        /// <summary>
        /// 计算总页码
        /// </summary>
        /// <param name="count"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static int CalculateTotalPages(int count,int pageSize)
        {
            return (int)Math.Ceiling((double)count / pageSize);
        }
    }
}
