using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_AutoMapper.ViewModels
{
    public class AuthorViewModel
    {
        //初始化newtonsoft.json的一个新实例。JsonPropertyAttribute类。
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "biography")]
        public string Biography { get; set; }

        [JsonProperty(PropertyName = "fullName")]
        public string FullName
        {
            get; set;
        }
    }
}
