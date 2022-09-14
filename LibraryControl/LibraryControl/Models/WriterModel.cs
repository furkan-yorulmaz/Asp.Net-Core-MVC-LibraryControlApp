using LibraryControl.Entity.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryControl.Models
{
    public class WriterModel
    {
        public List<Writer>? Writers { get; set; }

        [Required]
        public string WriterName { get; set; }
    }
}
