namespace BookLibrary.Data.Dtos
{
    public class AuthorForView
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
    }
}
