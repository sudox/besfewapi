using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoApi.Domain
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual IList<Book> Books { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

    }
}
