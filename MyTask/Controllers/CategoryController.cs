using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTask.Core.Models.Domains;
using MyTask.Core.Service;
using MyTask.Core.ViewModel;
using MyTask.Persistence.Extensions;

namespace MyTask.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {            
            _categoryService = categoryService;
        }

        public IActionResult ListOfCategory(bool show = false)
        {
            var userId = User.GetUserId();

            var vm = new ListOfCategoryViewModel
            {
                ShowNoActiv = show,
                Category = new CategoryViewModel
                {
                    Category = new Category
                    {
                        Id = 0,
                        UserId = userId,
                        IsActive = true
                    },
                    Heading = "Dodawanie nowej kategorii"
                },
                Categories = _categoryService.GetCategories(userId)
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeStatus(Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                    return RedirectToAction(nameof(ListOfCategory));

                var userId = User.GetUserId();
                _categoryService.ChangeStatus(category.Id, userId);
                return RedirectToAction(nameof(ListOfCategory));
            }
            catch
            {
                return View("Error");                
            }            
        }

        public IActionResult Category(int id = 0)
        {
            try
            {
                var userId = User.GetUserId();

                var category = id == 0 ?
                    new Category { Id = 0, UserId = userId, IsActive = true } :
                    _categoryService.Get(id, userId);

                var vm = new CategoryViewModel
                {
                    Category = category,
                    Heading = id == 0 ?
                    "Dodawanie nowej kategorii" : "Edytowanie kategorii",
                };

                return PartialView("_Category", vm);
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Category(Category category)
        {
            try
            {
                var userId = User.GetUserId();

                category.UserId = userId;

                if (!ModelState.IsValid)
                {
                    var vm = new ListOfCategoryViewModel
                    {
                        ShowNoActiv = false,
                        Category = new CategoryViewModel
                        {
                            Category = category,
                            Heading = category.Id == 0 ?
                            "Dodawanie nowej kategorii" : "Edytowanie kategorii",
                        },
                        Categories = _categoryService.GetCategories(userId)
                    };

                    return View(nameof(ListOfCategory), vm);
                }

                if (category.Id == 0)
                    _categoryService.Add(category);
                else
                    _categoryService.Update(category);

                return RedirectToAction(nameof(ListOfCategory));
            }
            catch
            {
                return View("Error");
            }
        }

        public IActionResult Delete(int id)
        {
            var userId = User.GetUserId();

            if (id <= 0)
                return RedirectToAction(nameof(ListOfCategory));

            var category = _categoryService.Get(id, userId);

            if (category.IsActive)
                return RedirectToAction(nameof(ListOfCategory));

            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            try
            {
                var userId = User.GetUserId();

                category.UserId = userId;

                if (!ModelState.IsValid)
                {
                    var vm = new ListOfCategoryViewModel
                    {
                        ShowNoActiv = false,
                        Category = new CategoryViewModel
                        {
                            Category = category,
                            Heading = "Edytowanie kategorii",
                        },
                        Categories = _categoryService.GetCategories(userId)
                    };

                    return View(nameof(ListOfCategory), vm);
                }

                _categoryService.Delete(category.Id, userId);

                return RedirectToAction(nameof(ListOfCategory));
            }
            catch
            {
                return View("Error");
            }
        }

    }
}
