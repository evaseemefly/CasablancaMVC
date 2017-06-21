using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CasablancaMVC.ViewModel
{
    public class ReturnData
    {
        public ReturnData(HttpStatusCode httpStatusCode,string content,string reaspnPhrase)
        {
            this.HttpStatueCode = httpStatusCode;
            this.Content = content;
            this.ReasonPhrase = reaspnPhrase;
        }

        /// <summary>
        /// 状态码
        /// </summary>
        public HttpStatusCode HttpStatueCode { get; private set; }

        /// <summary>
        /// 错误内容
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// 原因短语
        /// </summary>
        public string ReasonPhrase { get; private set; }
    }
}
