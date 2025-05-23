using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Models;

public class TodoList
{
    [Required]
    public List<TodoItem> Items { get; set; }
    public TodoList(){}
}