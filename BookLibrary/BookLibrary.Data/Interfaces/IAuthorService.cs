using BookLibrary.Data.Dtos;

namespace BookLibrary.Data.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorForView>> GetAllAuthorsAsync();
        Task<AuthorForView> GetAuthorByIdAsync(Guid authorId);
        Task<AuthorForView> AddAuthorAsync(AuthorCreateDto authorDto);
    }
}
