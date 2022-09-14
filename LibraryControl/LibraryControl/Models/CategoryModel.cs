using LibraryControl.Entity.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryControl.Models
{
    public class CategoryModel
    {
        public List<Category>? Categories { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
