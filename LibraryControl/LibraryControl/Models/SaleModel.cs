using LibraryControl.Entity.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryControl.Models
{
    public class SaleModel
    {
        [Required(ErrorMessage = "İsim alanı boş geçilemez.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyisim alanı boş geçilemez.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Telefon alanı boş geçilemez.")]
        [Display(Name = "05xxxxxxxxx")]
        [MaxLength(11, ErrorMessage = "Telefon numarası 11 haneli olmalıdır")]
        [MinLength(11, ErrorMessage = "Telefon numarası 11 haneli olmalıdır")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email alanı boş geçilemez.")]
        [Display(Name = "username@examle.com")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                           ErrorMessage = "Email adresi geçersiz.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Adres alanı boş geçilemez.")]
        public string Adress { get; set; }
        public int BookId { get; set; }
        public DateTime Date { get; set; }
        public bool RecordType { get; set; }
        public string BookImageUrl { get; set; }
        public string BookName { get; set; }
        public double BookPrice { get; set; }
    }
}
