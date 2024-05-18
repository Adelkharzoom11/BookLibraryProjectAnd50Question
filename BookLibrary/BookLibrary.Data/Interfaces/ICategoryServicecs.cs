using BookLibrary.Data.Dtos;

namespace BookLibrary.Data.Interfaces
{
    public interface ICategoryServicecs
    {
        Task<IEnumerable<CategoryForView>> GetAllCategoriesAsync();
        Task<CategoryForView> GetCategoryByIdAsync(Guid categoryId);
        Task<CategoryForView> AddCategoryAsync(CategoryCreateDto categoryDto);
    }
}
