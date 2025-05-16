using System;

namespace TodoList.Data;
      
using Microsoft.EntityFrameworkCore;
using TodoList.Models; // Assuming your TodoItem is in the Models namespace
using System.IO; // Required for Path

public class AppDbContext : DbContext
{
    public DbSet<Task> Tasks { get; set; }
    
    public string DbPath { get; }

    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var appDataPath = Path.Join(path, "TodoListApp");
        Directory.CreateDirectory(appDataPath);
        DbPath = Path.Join(appDataPath);
        DbPath = Path.Join(appDataPath, "todo.db");
        
    }
}