namespace TodoList.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
public class Goal
{
    [Key]
    public int ID { get; set; }
    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = String.Empty;
    public bool IsComplete { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; } = DateTime.Now;
    
}