using MyTask.Core.Models;
using MyTask.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTask.Core.ViewModel
{
    public class TasksViewModel
    {
        public IEnumerable<Task> Tasks { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public FilterTasks FilterTasks { get; set; }
    }
}
