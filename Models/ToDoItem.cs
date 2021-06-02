using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class ToDoItem
    {
        public ToDoItem()
        {

        }
        public ToDoItem(string task)
        {
            Id = Guid.NewGuid();
            Task = task;
        }
        public Guid Id { get; set; }
        public string Task { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
