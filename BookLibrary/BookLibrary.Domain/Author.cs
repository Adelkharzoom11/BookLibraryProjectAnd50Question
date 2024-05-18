using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookLibrary.Domain
{
    public class Author
    {
        [Key]
        public Guid AuthorId { get; set; }

        [Required(ErrorMessage = "يجب إدخال اسم المؤلف.")]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
        public string Country { get; set; }

        // Ignoring to avoid cyclic reference
        public ICollection<Book> Books { get; set; }
    }
}
