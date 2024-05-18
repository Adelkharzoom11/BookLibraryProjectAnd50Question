using BookLibrary.Data.Dtos;
using BookLibrary.Data.Interfaces;
using BookLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorForView>> GetAllAuthorsAsync()
        {
            var authors = await _context.Authors.AsNoTracking().ToListAsync();
            return authors.Select(author => new AuthorForView
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
                BirthDate = author.BirthDate,
                Country = author.Country
            }).ToList();
        }

        public async Task<AuthorForView> GetAuthorByIdAsync(Guid authorId)
        {
            var author = await _context.Authors.AsNoTracking().FirstOrDefaultAsync(a => a.AuthorId == authorId);
            if (author == null) return null;

            return new AuthorForView
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
                BirthDate = author.BirthDate,
                Country = author.Country
            };
        }

        public async Task<AuthorForView> AddAuthorAsync(AuthorCreateDto authorDto)
        {
            var author = new Author
            {
                AuthorId = Guid.NewGuid(),
                Name = authorDto.Name,
                BirthDate = authorDto.BirthDate,
                Country = authorDto.Country
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return new AuthorForView
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
                BirthDate = author.BirthDate,
                Country = author.Country
            };
        }
    }
}

