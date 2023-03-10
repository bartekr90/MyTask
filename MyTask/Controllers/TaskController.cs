using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTask.Core.Models;
using MyTask.Core.Models.Domains;
using MyTask.Core.Service;
using MyTask.Core.ViewModel;
using MyTask.Persistence;
using MyTask.Persistence.Extensions;
using MyTask.Persistence.Services;
using System;

namespace MyTask.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public IActionResult Tasks()
        {
            var userId = User.GetUserId();

            var vm = new TasksViewModel()
            {
                FilterTasks = new FilterTasks(),
                Tasks = _taskService.Get(userId),
                Categories = _taskService.GetCategories(userId)
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Tasks(TasksViewModel viewModel)
        {
            var userId = User.GetUserId();
            var tasks = _taskService.Get(userId,
                viewModel.FilterTasks.IsExecuted,
                viewModel.FilterTasks.CategoryId,
                viewModel.FilterTasks.Title);

            return PartialView("_TasksTable", tasks);
        }
        public IActionResult Task(int id = 0)
        {
            var userId = User.GetUserId();

            var task = id == 0 ?
                new Task { Id = 0, UserId = userId, Term = DateTime.Today } :
                _taskService.Get(id, userId);

            var vm = new TaskViewModel
            {
                Task = task,
                Heading = id == 0 ?
                "Dodawanie nowego zadania" : "Edytowanie zadania",
                Categories = _taskService.GetCategories(userId)
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Task(Task task)
        {
            var userId = User.GetUserId();
            task.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = new TaskViewModel
                {
                    Task = task,
                    Heading = task.Id == 0 ?
                "Dodawanie nowego zadania" : "Edytowanie zadania",
                    Categories = _taskService.GetCategories(userId)
                };

                return View("Task", vm);
            }

            if (task.Id == 0)
                _taskService.Add(task);
            else
                _taskService.Update(task);

            return RedirectToAction("Tasks");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _taskService.Delete(id, userId);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult Finish(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _taskService.Finish(id, userId);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }
    }
}
