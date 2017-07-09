using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class CartItem
    {

        public int Id { get; set; }

        public int CartId { get; set; }

        public int BookId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual Book Book { get; set; }
    }
}
