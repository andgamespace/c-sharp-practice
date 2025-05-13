// In TaskService.cs
namespace TodoList.Services;
using System;
using System.Collections.Generic;
using System.Linq;
// Note: NOT using System.Threading.Tasks directly
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

public class TaskService
{
    private readonly TaskContext _context;

    public TaskService()
    {
        _context = new TaskContext();
        _context.Database.EnsureCreated();
    }

    public async System.Threading.Tasks.Task<List<TodoList.Models.Task>> GetAllTasksAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async System.Threading.Tasks.Task<TodoList.Models.Task?> GetTaskAsync(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async System.Threading.Tasks.Task<TodoList.Models.Task?> GetTaskByIdAsync(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async System.Threading.Tasks.Task AddTaskAsync(TodoList.Models.Task task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task UpdateTaskAsync(TodoList.Models.Task task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }

    public async System.Threading.Tasks.Task ToggleTaskCompletionAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            task.IsCompleted = !task.IsCompleted;
            task.CompletedAt = task.IsCompleted ? DateTime.Now : (DateTime?)null;
            await _context.SaveChangesAsync();
        }
    }
}