using System;
using Avalonia.Controls.Shapes;

namespace TodoList.Data;
using Microsoft.EntityFrameworkCore;
using TodoList.Models;
using System.IO;

public class TaskContext : DbContext
{
    // dbContext: the  base class that represents a session with the database
    public DbSet<Models.Task> Tasks { get; set; } = null!;
    // Represents a collection of entities that can be queried from the database
    // null!: the "!" tells the compiler "I know this look snullable but trust me, it won't be null" 
    // null forgiving operator
    public string DbPath { get; }

    public TaskContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        // Gets the platform-specific local app data folder
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "todolist.db");
        //Combines paths in a cross-platform way
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) 
        => options.UseSqlite($"Data Source={DbPath}");
    // override method to configure the database connection
    // => shorthand for a single-statement method body
}