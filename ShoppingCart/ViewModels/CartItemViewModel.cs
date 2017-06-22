using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.ViewModels
{
    public class CartItemViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "cartId")]
        public int CartId { get; set; }

        [JsonProperty(PropertyName = "bookId")]
        public int BookId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "cart")]
        [Range(1,Int32.MaxValue,ErrorMessage ="总数必须比0大")]
        public virtual CartViewModel Cart { get; set; }

        [JsonProperty(PropertyName = "book")]
        public virtual BookViewModel Book { get; set; }
    }
}
