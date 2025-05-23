using System;
using TodoList.Enums;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Models;


public class TodoItem
{
   [Required]
   public string Title { get; set; } = string.Empty;
   public int Id { get; set; }
   public string Description { get; set; }
   public DateTime DateCreated { get; set; } = DateTime.Now;
   public DateTime DueDate { get; set; }
   public bool IsCompleted { get; set; }
   public Priority Priority { get; set; } // Low, Medium, High
   public TodoItem() { }
   public TodoItem(string title, string description, Priority priority, DateTime dueDate)
   {
      Title = title;
      Description = description;
      Priority = priority;
      DueDate = dueDate;
   }
   

}

