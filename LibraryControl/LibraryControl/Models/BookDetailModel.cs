using LibraryControl.Entity.Entities;

namespace LibraryControl.Models
{
    public class BookDetailModel
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public int? Sold { get; set; }
        public int? Leased{ get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Writer { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
    }
}
