using System;
using System.Collections.Generic;

namespace LibraryControl.Entity.Entities
{
    public partial class Writer
    {
        public Writer()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
