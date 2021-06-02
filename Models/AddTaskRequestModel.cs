using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class AddTaskRequestModel
    {
        [Required]
        public string Task { get; set; }
    }
}
