using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace TodoList.Models;
public class Task
{
    [Required]
    [MaxLength(150)]
    public string Title { get; set; }
    public int ID { get; set; }
    public bool IsComplete { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; } = DateTime.Now;

    public Task(int id, string title, bool isComplete, DateTime dueDate)
    {
        ID = id;
        Title = title;
        IsComplete = isComplete;
        CreatedAt = DateTime.Now;
        DueDate = dueDate;
    }
    
    public Task(int id, string title)
    {
        ID = id;
        Title = title;
        IsComplete = false;
        CreatedAt = DateTime.Now;
    }

    public Task()
    {
        ID = -999; 
        // -999 because all tasks should have IDs greater than 0
        Title = string.Empty;
        IsComplete = false;
        CreatedAt = DateTime.Now;
        DueDate = DateTime.Now;
    }
    

}