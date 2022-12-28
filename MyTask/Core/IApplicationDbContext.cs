using Microsoft.EntityFrameworkCore;
using MyTask.Core.Models.Domains;

namespace MyTask.Core
{
   public interface IApplicationDbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        int SaveChanges();
    }
}
