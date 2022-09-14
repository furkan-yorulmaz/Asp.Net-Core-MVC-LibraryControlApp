using System;
using System.Collections.Generic;

namespace LibraryControl.Entity.Entities
{
    public partial class Sale
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public int BookId { get; set; }
        public DateTime Date { get; set; }
        public bool RecordType { get; set; }

        public virtual Book Book { get; set; } = null!;
    }
}
