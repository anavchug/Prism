using Prism.Data;

namespace Prism.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public Task<Category> CreateCategoryAsync(Category category);
        public Task<Category> UpdateCategoryAsync(Category category);
        public Task<bool> DeleteCategoryAsync(int Id);
        public Task<Category> GetCategoryAsync(int Id);
        public Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
