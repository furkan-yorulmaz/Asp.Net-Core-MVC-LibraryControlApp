using System;
using System.Collections.Generic;

namespace LibraryControl.Entity.Entities
{
    public partial class Book
    {
        public Book()
        {
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public int Stock { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int WriterId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Writer Writer { get; set; } = null!;
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
