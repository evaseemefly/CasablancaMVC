using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasablancaMVC.Common
{
    public static class SerializationHelper
    {
        public static string HtmlConvertToJson(object model)
        {

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
           
            return JsonConvert.SerializeObject(model, settings);
        }
    }
}
