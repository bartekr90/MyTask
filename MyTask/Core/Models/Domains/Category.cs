using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MyTask.Core.Models.Domains
{
    public class Category
    {
        public Category()
        {
            Tasks = new Collection<Task>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "W użytku:")]
        public bool IsActive { get; set; } = true;

        [Required]
        [Display(Name = "Nazwa kategorii")]

        public string Name { get; set; }

        public ICollection<Task> Tasks { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
