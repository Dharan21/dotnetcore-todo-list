using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;
using ToDoList.Repository.Interfaces;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IOptions<ListSettings> _listSettingsOption;

        public HomeController(
            IToDoListRepository toDoListRepository,
            IOptions<ListSettings> listSettingsOption
        )
        {
            this._toDoListRepository = toDoListRepository;
            this._listSettingsOption = listSettingsOption;
        }

        public IActionResult Index()
        {
            int maxCapacity = this._listSettingsOption.Value.MaxCapacity;
            if (this._toDoListRepository.CheckOverflow(maxCapacity))
            {
                ViewData["IsOverFlow"] = true;
            }
            else
            {
                ViewData["IsOverFlow"] = false;
            }
            return View(this._toDoListRepository.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddTaskRequestModel model)
        {
            if (ModelState.IsValid)
            {
                this._toDoListRepository.Add(model.Task);
            }
            return RedirectToAction("Index");
        }

        public IActionResult CompleteTask(Guid id)
        {
            this._toDoListRepository.CompleteTask(id);
            return RedirectToAction("Index");
        }

        public IActionResult UndoCompleteTask(Guid id)
        {
            this._toDoListRepository.UndoComplete(id);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTask(Guid id)
        {
            this._toDoListRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
