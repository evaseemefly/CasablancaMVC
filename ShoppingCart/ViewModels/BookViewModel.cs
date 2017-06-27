using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShoppingCart.ViewModels
{
    public class BookViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "isbn")]
        public string Isbn { get; set; }

        /// <summary>
        /// 大纲
        /// </summary>
        [JsonProperty(PropertyName = "synopsis")]
        public string Synopsis { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "listPrice")]
        public decimal ListPrice { get; set; }

        [JsonProperty(PropertyName = "salePrice")]
        public decimal SalePrice { get; set; }

        /// <summary>
        /// 特定
        /// </summary>
        [JsonProperty(PropertyName = "featured")]
        public bool Featured { get; set; }

        [JsonProperty(PropertyName = "savePercentage")]
        public int SavePercentage
        {
            get
            {
                return (int)(100 - (SalePrice / ListPrice * 100));
            }
        }

        [JsonProperty(PropertyName = "author")]
        public virtual AuthorViewModel Author { get; set; }

        /// <summary>
        /// 种类
        /// </summary>
        [JsonProperty(PropertyName = "category")]
        public virtual CategoryViewModel Category { get; set; }


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

        //public virtual Author Author { get; set; }

        ///// <summary>
        ///// 种类
        ///// </summary>
        //public virtual Category Category { get; set; }

    }
}
