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
        [JsonProperty(PropertyName ="queryOptions")]
        public QueryOptions QueryOptions { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<T> Result { get; set; }
    }
}
