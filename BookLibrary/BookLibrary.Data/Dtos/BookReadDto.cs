using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Data.Dtos
{
    public class BookReadDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile CoverImageFile { get; set; }

        [Required]
        public IFormFile BookFile { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
