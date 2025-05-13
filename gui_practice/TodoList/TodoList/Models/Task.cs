using System;

namespace TodoList.Models;

public class Task
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? CompletedAt { get; set; }
    public Priority Priority { get; set; } = Priority.Normal;

}
public enum Priority
{
    Low,
    Normal,
    High
}
