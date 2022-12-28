using MyTask.Core.Models.Domains;
using System.Collections.Generic;

namespace MyTask.Core.ViewModel
{
    public class ListOfCategoryViewModel
    {
        public CategoryViewModel Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public bool ShowNoActiv { get; set; } = false;

    }
}
