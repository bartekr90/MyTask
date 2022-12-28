using MyTask.Core.Models.Domains;
using System.Collections.Generic;

namespace MyTask.Core.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories(string userId);

        Category Get(int id, string userId);

        void Add(Category category);

        void Update(Category category);

        void Delete(int id, string userId);
        void ChangeStatus(int id, string userId);
    }
}
