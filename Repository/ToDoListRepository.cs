using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;
using ToDoList.Repository.Interfaces;

namespace ToDoList.Repository
{
    public class ToDoListRepository : IToDoListRepository
    {
        private IList<ToDoItem> _list;
        public ToDoListRepository()
        {
            _list = new List<ToDoItem>
            {
                new ToDoItem("Pay Electicity Bill"),
                new ToDoItem("Meet Kevin"),
                new ToDoItem("CheckIn Code"),
            };
        }

        public void Add(string task)
        {
            this._list.Add(new ToDoItem(task));
        }

        public bool CheckOverflow(int maxCapacity)
        {
            return this._list.Count >= maxCapacity;
        }

        public void ToggleCompleteTask(Guid id)
        {
            var task = this._list.FirstOrDefault(l => l.Id == id);
            task.IsCompleted = !task.IsCompleted;
        }

        public void Delete(Guid id)
        {
            var task = this._list.FirstOrDefault(l => l.Id == id);
            this._list.Remove(task);
        }

        public IList<ToDoItem> GetAll()
        {
            return this._list;
        }
    }
}
