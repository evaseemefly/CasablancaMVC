using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Models
{
    /// <summary>
    /// 货运
    /// </summary>
    public class Cart
    {
        public int Id { get; set; }


        [Index(IsUnique =true)]
        [StringLength(255)]
        public string SessionId { get; set; }
        

        public virtual ICollection<CartItem> CartItems { get; set; }

    }
}
