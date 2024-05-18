using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Data.Dtos
{
    public class AuthorCreateDto
    {
        [Required(ErrorMessage = "يجب إدخال اسم المؤلف.")]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
    }
}
