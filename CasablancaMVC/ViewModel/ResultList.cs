using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;
using CasablancaMVC.Models;

namespace CasablancaMVC.ViewModel
{
    public class ResultList<T>
    {
        /// <summary>
        /// 新增的构造函数
        /// QueryOptions,Result均为可读，只在构造函数中可为其赋值
        /// </summary>
        /// <param name="result"></param>
        /// <param name="queryOptions"></param>
        public ResultList(List<T> result,QueryOptions queryOptions)
        {
            Result = result;
            QueryOptions = queryOptions;
            
        }

        [JsonProperty(PropertyName ="queryOptions")]
        public QueryOptions QueryOptions { get;private set; }

        /// <summary>
        /// 结果集合
        /// </summary>
        [JsonProperty(PropertyName = "results")]
        public List<T> Result { get;private set; }
    }
}
