using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Components
{
    public class ToDoItemViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ToDoItem item)
        {
            return await Task.FromResult(View(item));
        }
    }
}
