using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Repository.Interfaces;

namespace ToDoList.Components
{
    public class ToDoListViewComponent : ViewComponent
    {
        private readonly IToDoListRepository _toDoListRepository;

        public ToDoListViewComponent(IToDoListRepository toDoListRepository)
        {
            this._toDoListRepository = toDoListRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(this._toDoListRepository.GetAll()));
        }
    }
}
