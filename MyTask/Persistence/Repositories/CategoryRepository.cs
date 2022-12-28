using MyTask.Core;
using MyTask.Core.Models.Domains;
using MyTask.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MyTask.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private IApplicationDbContext _context;
        public CategoryRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories(string userId)
        {
            return _context.Categories
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.IsActive)
                .ThenBy(x => x.Name)
                .ToList();
        }

        public Category Get(int id, string userId)
        {
            var category = _context.Categories
               .Single(x => x.Id == id && x.UserId == userId);
            return category;
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Delete(int id, string userId)
        {
            var categoryToDelete = _context.Categories
                .Single(x => x.Id == id && x.UserId == userId);

            _context.Categories.Remove(categoryToDelete);
        }
         

        public void Update(Category category)
        {
            var categoryToUpdate = _context.Categories
                .Single(x => x.Id == category.Id);
            
            categoryToUpdate.Name = category.Name;           
        }

        public void ChangeStatus(int id, string userId)
        {
            var category = _context.Categories
                .Single(x => x.Id == id && x.UserId == userId);
            category.IsActive = !category.IsActive;
        }
    }
}
