namespace TodoList.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Models;
public interface ITaskService
{
    Task<List<Models.Task>> GetTasksAsync();
    Task AddTaskAsync(Models.Task task);
    Task (Models.Task task);
    Task 
}