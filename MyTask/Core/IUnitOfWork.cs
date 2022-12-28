using MyTask.Core.Repositories;

namespace MyTask.Core
{
    public interface IUnitOfWork
    {
        ITaskRepository Task { get; }

        ICategoryRepository Category { get; }

        void Complete();

    }
}
