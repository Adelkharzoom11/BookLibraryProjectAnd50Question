using BookLibrary.Data.Dtos;

namespace BookLibrary.Data.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookForView>> GetAllBooksAsync();
        Task<BookForView> GetBookByIdAsync(Guid bookId);
        Task<IEnumerable<BookForView>> GetBooksByCategoryAsync(string categoryName);
        Task<BookForView> AddBookAsync(BookReadDto bookDto);
        Task<byte[]> DownloadBookAsync(Guid bookId);
    }
}
