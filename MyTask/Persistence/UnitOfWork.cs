using MyTask.Core;
using MyTask.Core.Repositories;
using MyTask.Persistence.Repositories;

namespace MyTask.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;
        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;
            Task = new TaskRepository(context);
            Category = new CategoryRepository(context);
        }

        public ITaskRepository Task { get; set; }
        public ICategoryRepository Category { get; set; }
        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
