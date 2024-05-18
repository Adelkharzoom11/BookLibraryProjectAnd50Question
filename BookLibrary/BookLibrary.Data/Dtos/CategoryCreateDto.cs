
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Data.Dtos
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}