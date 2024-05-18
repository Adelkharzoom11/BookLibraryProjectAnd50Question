using BookLibrary.Data.Dtos;
using BookLibrary.Data.Interfaces;
using BookLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data.Services
{
    public class CategoryService : ICategoryServicecs
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryForView>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();
            return categories.Select(category => new CategoryForView
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            }).ToList();
        }

        public async Task<CategoryForView> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            if (category == null) return null;

            return new CategoryForView
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };
        }

        public async Task<CategoryForView> AddCategoryAsync(CategoryCreateDto categoryDto)
        {
            var category = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryDto.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryForView
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };
        }
    }
}

