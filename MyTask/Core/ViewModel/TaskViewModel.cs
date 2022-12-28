using MyTask.Core.Models.Domains;
using System.Collections.Generic;

namespace MyTask.Core.ViewModel
{
    public class TaskViewModel
    {
        public string Heading { get; set; }
        public Task Task { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
