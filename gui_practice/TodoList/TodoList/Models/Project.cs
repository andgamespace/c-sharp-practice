using System;
using System.Collections.Generic;
using TodoList.Models;
namespace TodoList.Models;

public class Project
{
    public string Title { get; set; } = string.Empty;
    public int ID { get; set; } = -999; // -999 because all projects should have IDs greater than 0
    public bool IsComplete { get; set; } = false;
    public string Description { get; set; } = string.Empty;
    public List<Task> Tasks { get; set; } = new List<Task>();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime DueDate{ get; set; } = DateTime.Now;
    public Project(String title, int ID, DateTime CreatedAt, DateTime DueDate, List<Task> Tasks)
    {
        Title = title;
        IsComplete = false;
    }
    public Project(String title, DateTime dueDate)
    {
        Title = title;
        IsComplete = false;
        CreatedAt = DateTime.Now;
        DueDate = dueDate;
    }
    
    
}