using MyTask.Core;
using MyTask.Core.Models.Domains;
using MyTask.Core.Service;
using System.Collections.Generic;

namespace MyTask.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetCategories(string userId)
        {
            return _unitOfWork.Category.GetCategories(userId);
        }

        public Category Get(int id, string userId)
        {
            return _unitOfWork.Category.Get(id, userId);
        }

        public void Add(Category category)
        {
            _unitOfWork.Category.Add(category);
            _unitOfWork.Complete();
        }

        public void Delete(int id, string userId)
        {
            _unitOfWork.Category.Delete(id, userId);
            _unitOfWork.Complete();
        }

        public void Update(Category category)
        {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Complete();
        }

        public void ChangeStatus(int id, string userId)
        {
            _unitOfWork.Category.ChangeStatus(id, userId);
            _unitOfWork.Complete();
        }
    }
}
