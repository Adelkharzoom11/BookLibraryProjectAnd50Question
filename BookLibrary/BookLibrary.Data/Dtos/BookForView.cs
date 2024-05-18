using BookLibrary.Domain;

namespace BookLibrary.Data.Dtos
{
    public class BookForView
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImagePath { get; set; }
        public string BookFilePath { get; set; }
        public AuthorForView Author { get; set; }
        public CategoryForView Category
        {
            get; set;
        }
    }
}
