using LibraryControl.Entity.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryControl.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Stok bilgisi boş geçilemez.")]
        public int? Stock { get; set; }

        [Required(ErrorMessage = "Ürün isim bilgisi boş geçilemez.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Açıklama bilgisi boş geçilemez.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Fiyat bilgisi boş geçilemez.")]
        public double? Price { get; set; }
        public int WriterId { get; set; }
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
