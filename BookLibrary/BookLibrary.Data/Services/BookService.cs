using BookLibrary.Data.Dtos;
using BookLibrary.Data.Interfaces;
using BookLibrary.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _storagePath;

        public BookService(ApplicationDbContext context, string storagePath)
        {
            _context = context;
            _storagePath = storagePath;
            EnsureStoragePathExists(_storagePath);
        }

        private void EnsureStoragePathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public async Task<BookForView> GetBookByIdAsync(Guid bookId)
        {
            var book = await _context.Books.AsNoTracking()
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.BookId == bookId);

            if (book == null) return null;

            return new BookForView
            {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                CoverImagePath = book.CoverImagePath,
                BookFilePath = book.BookFilePath,
                Author = new AuthorForView
                {
                    AuthorId = book.Author.AuthorId,
                    Name = book.Author.Name,
                    BirthDate = book.Author.BirthDate,
                    Country = book.Author.Country
                },
                Category = new CategoryForView
                {
                    CategoryId = book.Category.CategoryId,
                    Name = book.Category.Name
                }
            };
        }

        public async Task<IEnumerable<BookForView>> GetAllBooksAsync()
        {
            var books = await _context.Books.AsNoTracking()
                .Include(b => b.Author)
                .Include(b => b.Category)
                .ToListAsync();

            return books.Select(book => new BookForView
            {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                CoverImagePath = book.CoverImagePath,
                BookFilePath = book.BookFilePath,
                Author = new AuthorForView
                {
                    AuthorId = book.Author.AuthorId,
                    Name = book.Author.Name,
                    BirthDate = book.Author.BirthDate,
                    Country = book.Author.Country
                },
                Category = new CategoryForView
                {
                    CategoryId = book.Category.CategoryId,
                    Name = book.Category.Name
                }
            }).ToList();
        }

        public async Task<IEnumerable<BookForView>> GetBooksByCategoryAsync(string categoryName)
        {
            var books = await _context.Books.AsNoTracking()
                .Where(b => b.Category.Name == categoryName)
                .Include(b => b.Author)
                .Include(b => b.Category)
                .ToListAsync();

            return books.Select(book => new BookForView
            {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                CoverImagePath = book.CoverImagePath,
                BookFilePath = book.BookFilePath,
                Author = new AuthorForView
                {
                    AuthorId = book.Author.AuthorId,
                    Name = book.Author.Name,
                    BirthDate = book.Author.BirthDate,
                    Country = book.Author.Country
                },
                Category = new CategoryForView
                {
                    CategoryId = book.Category.CategoryId,
                    Name = book.Category.Name
                }
            }).ToList();
        }

        public async Task<BookForView> AddBookAsync(BookReadDto bookDto)
        {
            try
            {
                // Save cover image to storage and get its URL
                string coverImageUrl = await SaveFileToStorage(bookDto.CoverImageFile);

                // Save book file to storage and get its URL
                string bookDownloadUrl = await SaveFileToStorage(bookDto.BookFile);

                var book = new Book
                {
                    BookId = Guid.NewGuid(),
                    Title = bookDto.Title,
                    Description = bookDto.Description,
                    CoverImagePath = coverImageUrl,
                    BookFilePath = bookDownloadUrl,
                    AuthorId = bookDto.AuthorId,
                    CategoryId = bookDto.CategoryId
                };
                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                return new BookForView
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Description = book.Description,
                    CoverImagePath = book.CoverImagePath,
                    BookFilePath = book.BookFilePath,
                    Author = new AuthorForView
                    {
                        AuthorId = book.Author.AuthorId,
                        Name = book.Author.Name,
                        BirthDate = book.Author.BirthDate,
                        Country = book.Author.Country
                    },
                    Category = new CategoryForView
                    {
                        CategoryId = book.Category.CategoryId,
                        Name = book.Category.Name
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding book", ex);
            }
        }

        public async Task<byte[]> DownloadBookAsync(Guid bookId)
        {
            var book = await _context.Books.FindAsync(bookId);

            if (book == null)
            {
                throw new Exception("الكتاب غير موجود.");
            }

            string filePath = book.BookFilePath;

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new Exception("لا يمكن العثور على ملف الكتاب.");
            }

            return await File.ReadAllBytesAsync(filePath);
        }

        private async Task<string> SaveFileToStorage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("لا يوجد ملف للحفظ.");
            }

            string fileName = $"{Guid.NewGuid()}_{file.FileName}";
            string filePath = Path.Combine(_storagePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}

