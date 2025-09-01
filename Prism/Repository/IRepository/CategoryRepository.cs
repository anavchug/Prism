using Microsoft.EntityFrameworkCore;
using Prism.Data;

namespace Prism.Repository.IRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _db.Category.AddAsync(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int Id)
        {
            var category = await _db.Category.FirstOrDefaultAsync(c => c.Id == Id);
            if (category != null)
            {
                _db.Category.Remove(category);
                return await _db.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _db.Category.ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int Id)
        {
            var category = await _db.Category.FirstOrDefaultAsync(c => c.Id == Id);

            if (category != null)
            {
                return category;
            }
            return new Category();
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _db.Category.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                _db.Update(existingCategory);
                await _db.SaveChangesAsync();
                return existingCategory;
            }

            return category;
        }
    }
}
