using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Domain
{
    public class Book
    {
        [Key]
        public Guid BookId { get; set; }

        [Required(ErrorMessage = "يجب إدخال عنوان الكتاب.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "يجب إدخال تاريخ نشر الكتاب.")]
        public DateTime PublishedDate { get; set; }

        [Required(ErrorMessage = "يجب إدخال وصف الكتاب.")]
        public string Description { get; set; }

        public string CoverImagePath { get; set; }
        public string BookFilePath { get; set; }

        public Guid AuthorId { get; set; }
        public Author Author { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
