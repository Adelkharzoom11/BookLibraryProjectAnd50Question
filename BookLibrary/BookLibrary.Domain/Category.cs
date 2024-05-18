using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookLibrary.Domain
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "يجب إدخال اسم تصنيف الكتاب.")]
        public string Name { get; set; }

        // Ignoring to avoid cyclic reference
        public ICollection<Book> Books { get; set; }
    }
}
