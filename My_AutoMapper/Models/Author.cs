using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_AutoMapper.Models
{
    public class Author
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Biography { get; set; }


        
        public string FullName
        {
            get
            {
                return FirstName + ' ' + LastName;
            }
        }

        public virtual ICollection<Book> Books { get; set; }
    }
}
