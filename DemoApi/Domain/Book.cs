using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Domain
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        
        public Author Author { get; set; }
        public virtual IList<Review> Reviews { get; set; }
    }
}
