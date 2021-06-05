using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Repository.Interfaces
{
    public interface IToDoListRepository
    {
        IList<ToDoItem> GetAll();
        void Add(string task);
        void Delete(Guid id);
        void ToggleCompleteTask(Guid id);
        bool CheckOverflow(int maxCapacity);
    }
}
